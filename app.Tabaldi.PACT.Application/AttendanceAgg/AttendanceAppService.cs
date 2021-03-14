using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using System;
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
        Task<string> ExecuteQueueAsync();
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

        public async Task<string> ExecuteQueueAsync()
        {
            var clients = await _clientRepository.RetrieveAsync(null, false, p => p.Recurrences);

            foreach (var client in clients)
            {
                foreach (var attendanceRecurrence in client.Recurrences)
                {
                    var yesterday = DateTime.Now.AddDays(-1);

                    var attendanceExists = await Repository.AnyAsync(AttendanceSpecifications.RetrieveByClientIDAndDate(yesterday, client.ID));
                    if (attendanceExists) { continue; }

                    if (yesterday.DayOfWeek == (DayOfWeek)attendanceRecurrence.WeekDay)
                    {
                        var attendance = new Attendance(client.ID, yesterday, attendanceRecurrence.StartTime, attendanceRecurrence.EndTime, string.Empty);

                        Repository.Create(attendance);
                    }
                }
            }

            await CommitAsync();

            return Guid.NewGuid().ToString();
        }
    }
}
