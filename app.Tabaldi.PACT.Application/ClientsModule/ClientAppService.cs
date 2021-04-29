using app.Tabaldi.PACT.Application.ClientsModule.Models;
using app.Tabaldi.PACT.Crosscutting.NetCore.AuthenticatedUser;
using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Commands;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.ClientsModule
{
    public interface IClientAppService
    {
        Task<int> CreateAsync(ClientAddCommand command);
        IQueryable<ClientModel> RetrieveAll();
        Task<ClientModel> GetByIdAsync(int clientId);
        Task<bool> UpdateAsync(ClientEditCommand command);
        Task<bool> RemoveAsync(ClientRemoveCommand command);
    }

    public class ClientAppService : AppServiceBase<IClientRepository>, IClientAppService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly Lazy<IAuthenticatedUser> _authenticatedUser;

        public ClientAppService(
            IAttendanceRepository attendanceRepository,
            IClientRepository repository,
            IUnitOfWork unitOfWork,
            Lazy<IAuthenticatedUser> authenticatedUser)
            : base(repository, unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<int> CreateAsync(ClientAddCommand command)
        {
            var exists = await Repository.AnyAsync(ClientSpecifications.RetrieveByName(command.Name));
            Guard.ObjectAlreadyExists<Client>(exists);

            var client = new Client(command.Name, command.DateOfBirth, command.Phone, command.ClinicalDiagnosis, command.PhysiotherapeuticDiagnosis, command.TreatmentConduct, command.Objectives, command.ChargingType, command.Value, _authenticatedUser.Value.User.ID);

            client.AddRecurrences(command.Recurrences.Select(p => new AttendanceRecurrence(p.WeekDay, p.StartTime, p.EndTime, client)));

            var obj = Repository.Create(client);

            await CommitAsync();

            return obj.ID;
        }

        public Task<ClientModel> GetByIdAsync(int clientId)
        {
            return Repository.SingleOrDefaultAsync(new ClientModelMapper(ClientSpecifications.RetrieveByID(clientId)));
        }

        public async Task<bool> RemoveAsync(ClientRemoveCommand command)
        {
            foreach (var clientId in command.IDs)
            {
                var client = await Repository.SingleOrDefaultAsync(ClientSpecifications.RetrieveByID(clientId), true);
                client.Recurrences.Clear();
                var attendances = await _attendanceRepository.RetrieveAsync(AttendanceSpecifications.RetrieveByClientID(clientId));
                await _attendanceRepository.DeleteAsync(attendances.Select(p => p.ID).ToArray());
            }

            await Repository.DeleteAsync(command.IDs);

            return await CommitAsync();
        }

        public IQueryable<ClientModel> RetrieveAll()
        {
            return Repository.RetrieveMapper(new ClientModelMapper(ClientSpecifications.RetrieveByUserID(_authenticatedUser.Value.User.ID)));
        }

        public async Task<bool> UpdateAsync(ClientEditCommand command)
        {
            var client = await Repository.SingleOrDefaultAsync(ClientSpecifications.RetrieveByID(command.ID), true);

            client.SetName(command.Name);
            client.SetDateOfBirth(command.DateOfBirth);
            client.SetDiagnosis(command.ClinicalDiagnosis, command.PhysiotherapeuticDiagnosis);
            client.SetPhone(command.Phone);
            client.SetObjective(command.Objectives);
            client.SetCosts(command.Value, command.ChargingType);
            client.SetTreatmentConduct(command.TreatmentConduct);
            client.SetEnabled(command.Enabled);

            client.Recurrences.Clear();
            client.AddRecurrences(command.Recurrences.Select(p => new AttendanceRecurrence(p.WeekDay, p.StartTime, p.EndTime, client)));

            return await CommitAsync();
        }
    }
}
