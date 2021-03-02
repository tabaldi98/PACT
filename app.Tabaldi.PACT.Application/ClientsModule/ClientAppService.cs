using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.ClientsModule
{
    public interface IClientAppService
    {
        Task<int> CreateAsync(ClientAddCommand command);
        IQueryable<ClientModel> RetrieveAll();
        Task<bool> UpdateAsync(ClientEditCommand command);
        Task<bool> RemoveAsync(ClientRemoveCommand command);
    }

    public class ClientAppService : AppServiceBase<IClientRepository>, IClientAppService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public ClientAppService(
            IAttendanceRepository attendanceRepository,
            IClientRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            this._attendanceRepository = attendanceRepository;
        }

        public async Task<int> CreateAsync(ClientAddCommand command)
        {
            var exists = await Repository.AnyAsync(ClientSpecifications.RetrieveByName(command.Name));
            Guard.ObjectAlreadyExists<Client>(exists);

            var client = new Client(command.Name, command.Diagnosis, command.DateOfBirth, command.Phone, command.Objective);
            client.SetCosts(command.Value, command.ChargingType);

            foreach (var recurrence in command.Recurrences)
            {
                client.AddRecurrence(new AttendanceRecurrence(recurrence.WeekDay, recurrence.StartTime, recurrence.EndTime, client));
            }

            var obj = Repository.Create(client);

            await CommitAsync();

            return obj.ID;
        }

        public async Task<bool> RemoveAsync(ClientRemoveCommand command)
        {
            foreach (var clientId in command.IDs)
            {
                var attendances = await _attendanceRepository.RetrieveAsync(AttendanceSpecifications.RetrieveByClientID(clientId));
                await _attendanceRepository.DeleteAsync(attendances.Select(p => p.ID).ToArray());
            }

            await Repository.DeleteAsync(command.IDs);

            return await CommitAsync();
        }

        public IQueryable<ClientModel> RetrieveAll()
        {
            return Repository.RetrieveMapper(new ClientModelMapper());
        }

        public async Task<bool> UpdateAsync(ClientEditCommand command)
        {
            var client = await Repository.SingleOrDefaultAsync(ClientSpecifications.RetrieveByID(command.ID), true);

            client.SetName(command.Name);
            client.SetDateOfBirth(command.DateOfBirth);
            client.SetDiagnosis(command.Diagnosis);
            client.SetPhone(command.Phone);
            client.SetObjective(command.Objective);
            client.SetCosts(command.Value, command.ChargingType);

            client.Recurrences.Clear();

            foreach (var recurrence in command.Recurrences)
            {
                client.AddRecurrence(new AttendanceRecurrence(recurrence.WeekDay, recurrence.StartTime, recurrence.EndTime, client));
            }

            return await CommitAsync();
        }
    }
}
