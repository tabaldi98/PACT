using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;

namespace app.Tabaldi.PACT.Application.ClientsModule
{
    public interface IClientAppService
    {
        Task<int> CreateAsync(ClientAddCommand command);
        Task<IQueryable<ClientModel>> RetrieveAllAsync();
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
            SetDays(client, command);

            var obj = Repository.Create(client);

            await CommitAsync();

            return obj.ID;
        }

        private void SetDays(Client client, IClientCommandBase command)
        {
            if (command.DaysOffAttendance.Any(p => p.Equals("Segunda-feira")))
            { client.SetServiceAsMonday(command.StartMonday, command.EndMonday); }
            else
            { client.SetNoServiceAsMonday(); }

            if (command.DaysOffAttendance.Any(p => p.Equals("Terça-feira")))
            { client.SetServiceAsTuesday(command.StartTuesday, command.EndTuesday); }
            else { client.SetNoServiceAsTuesday(); }

            if (command.DaysOffAttendance.Any(p => p.Equals("Quarta-feira")))
            { client.SetServiceAsWednesday(command.StartWednesday, command.EndWednesday); }
            else { client.SetNoServiceAsWednesday(); }

            if (command.DaysOffAttendance.Any(p => p.Equals("Quinta-feira")))
            { client.SetServiceAsThursday(command.StartThursday, command.EndThursday); }
            else { client.SetNoServiceAsThursday(); }

            if (command.DaysOffAttendance.Any(p => p.Equals("Sexta-feira")))
            { client.SetServiceAsFriday(command.StartFriday, command.EndFriday); }
            else { client.SetNoServiceAsFriday(); }

            if (command.DaysOffAttendance.Any(p => p.Equals("Sábado")))
            { client.SetServiceAsSaturday(command.StartSaturday, command.EndSaturday); }
            else { client.SetNoServiceAsSaturday(); }

            if (command.DaysOffAttendance.Any(p => p.Equals("Domingo")))
            { client.SetServiceAsSunday(command.StartSunday, command.EndSunday); }
            else { client.SetNoServiceAsSunday(); }
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

        // TODO: Alterar para mapper. Essa var DaysOffAttendance trocar para deixar igual ao domain
        public async Task<IQueryable<ClientModel>> RetrieveAllAsync()
        {
            var clients = await Repository.RetrieveAsync();

            return clients.Select(p => new ClientModel()
            {
                ID = p.ID,
                Name = p.Name,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth,
                Diagnosis = p.Diagnosis,
                Objective = p.Objective,
                ChargingType = p.ChargingType,
                Value = p.Value,
                DaysOffAttendance = MapDays(p),
                StartMonday = p.StartMonday,
                EndMonday = p.EndMonday,
                StartTuesday = p.StartTuesday,
                EndTuesday = p.EndTuesday,
                StartWednesday = p.StartWednesday,
                EndWednesday = p.EndWednesday,
                StartThursday = p.StartThursday,
                EndThursday = p.EndThursday,
                StartFriday = p.StartFriday,
                EndFriday = p.EndFriday,
                StartSaturday = p.StartSaturday,
                EndSaturday = p.EndSaturday,
                StartSunday = p.StartSunday,
                EndSunday = p.EndSunday,
                RegistrationDate = p.RegistrationDate,
            }).AsQueryable();
        }

        private IList<string> MapDays(Client client)
        {
            var list = new List<string>();

            if (client.HasServiceOnMonday) { list.Add("Segunda-feira"); }
            if (client.HasServiceOnTuesday) { list.Add("Terça-feira"); }
            if (client.HasServiceOnWednesday) { list.Add("Quarta-feira"); }
            if (client.HasServiceOnThursday) { list.Add("Quinta-feira"); }
            if (client.HasServiceOnFriday) { list.Add("Sexta-feira"); }
            if (client.HasServiceOnSaturday) { list.Add("Sábado"); }
            if (client.HasServiceOnSunday) { list.Add("Domingo"); }

            return list;
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
            SetDays(client, command);

            return await CommitAsync();
        }
    }
}
