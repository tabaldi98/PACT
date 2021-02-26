using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.AttendanceAgg
{
    public interface IAttendanceAppService
    {
        Task<int> CreateAsync(AttendanceAddCommand command);
        IQueryable<AttendanceModel> RetrieveAllByClientID(int clientId);
        Task<bool> RemoveAsync(AttendanceRemoveCommand command);
        Task<bool> UpdateAsync(AttendanceEditCommand command);
    }

    public class AttendanceAppService : AppServiceBase<IAttendanceRepository>, IAttendanceAppService
    {
        public AttendanceAppService(
            IAttendanceRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        { }

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
    }
}
