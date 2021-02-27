using System.Linq;
using System.Threading.Tasks;
using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;

namespace app.Tabaldi.PACT.Application.AttendanceAgg
{
    public interface IAttendanceAppService
    {
        Task<int> CreateAsync(AttendanceAddCommand command);
        IQueryable<AttendanceModel> RetrieveAllByClientID(int clientId);
        Task<bool> RemoveAsync(AttendanceRemoveCommand command);
        Task<bool> UpdateAsync(AttendanceEditCommand command);

        IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances();
        IQueryable<AttendancesCurrentWeekModel> GetCurrentWeekAttendances();
    }

    public class AttendanceAppService : AppServiceBase<IAttendanceRepository>, IAttendanceAppService
    {
        private readonly IClientRepository _clientRepository;

        public AttendanceAppService(
            IClientRepository clientRepository,
            IAttendanceRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> CreateAsync(AttendanceAddCommand command)
        {
            var attendance = new Attendance(command.ClientID, command.Date, command.HourInitial, command.HourFinish, command.Description);

            Repository.Create(attendance);

            await CommitAsync();

            return attendance.ID;
        }

        public IQueryable<AttendanceModel> RetrieveAllByClientID(int clientId)
        {
            return Repository.RetrieveMapper(new AttendanceModelMapper(clientId));
        }

        public async Task<bool> RemoveAsync(AttendanceRemoveCommand command)
        {
            await Repository.DeleteAsync(command.IDs);

            return await CommitAsync();
        }

        public async Task<bool> UpdateAsync(AttendanceEditCommand command)
        {
            var attendance = await Repository.SingleOrDefaultAsync(AttendanceSpecifications.RetrieveByID(command.ID), true);
            Guard.ObjectNotFound(attendance);

            attendance.SetDate(command.Date, command.HourInitial, command.HourFinish);
            attendance.SetDescription(command.Description);

            return await CommitAsync();
        }

        public IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances()
        {
            return _clientRepository.RetrieveMapper(new AttendancesCurrentDayModelMapper());
        }

        IQueryable<AttendancesCurrentWeekModel> IAttendanceAppService.GetCurrentWeekAttendances()
        {
            return _clientRepository.RetrieveMapper(new AttendancesCurrentWeekModelMapper());
        }
    }
}
