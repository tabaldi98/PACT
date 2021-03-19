using app.Tabaldi.PACT.Crosscutting.NetCore.Extensions;
using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using System;
using System.Linq;
using System.Threading.Tasks;
using app.Tabaldi.PACT.Crosscutting.NetCore.Emails;
using System.Text;
using Microsoft.Extensions.Logging;

namespace app.Tabaldi.PACT.Application.AttendanceAgg
{
    public interface IAttendanceAppService
    {
        Task<int> CreateAsync(AttendanceAddCommand command);
        IQueryable<AttendanceModel> RetrieveAllByClientID(int clientId);
        Task<bool> RemoveAsync(AttendanceRemoveCommand command);
        Task<bool> UpdateAsync(AttendanceEditCommand command);

        Task<string> ExecuteQueueAsync();
        Task<string> ExecuteAlertQueueAsync();
    }

    public class AttendanceAppService : AppServiceBase<IAttendanceRepository>, IAttendanceAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AttendanceAppService> _logger;

        public AttendanceAppService(
            IClientRepository clientRepository,
            IAttendanceRepository repository,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ILogger<AttendanceAppService> logger)
            : base(repository, unitOfWork)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _logger = logger;
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
            //var clients = await _clientRepository.RetrieveAsync(null, false, p => p.Recurrences);

            //foreach (var client in clients)
            //{
            //    foreach (var attendanceRecurrence in client.Recurrences)
            //    {
            //        var yesterday = DateTime.Now.AddDays(-1);

            //        var attendanceExists = await Repository.AnyAsync(AttendanceSpecifications.RetrieveByClientIDAndDate(yesterday, client.ID));
            //        if (attendanceExists) { continue; }

            //        if (yesterday.DayOfWeek == (DayOfWeek)attendanceRecurrence.WeekDay)
            //        {
            //            var attendance = new Attendance(client.ID, yesterday, attendanceRecurrence.StartTime, attendanceRecurrence.EndTime, string.Empty);

            //            Repository.Create(attendance);
            //        }
            //    }
            //}

            //await CommitAsync();

            return bool.TrueString;
        }

        public async Task<string> ExecuteAlertQueueAsync()
        {
            Log("Iniciando processamento");
            var dtNow = DateTime.Now;

            var usersAlerts = await _userRepository.RetrieveAsync(UserSpecification.RetrieveUserAlertsEnabled(), false, x => x.Clients);

            Log($"Buscou {usersAlerts.Count} usuários com alertas habilitados");

            foreach (var user in usersAlerts)
            {
                Log($"Iniciando processamento do usuário {user.UserName} com {user.Clients.Count} clientes.");
                foreach (var client in user.Clients)
                {
                    Log($"Iniciando processamento do cliente {client.Name}");

                    // Valida se tem atendimento hoje
                    var attendanceToday = client.Recurrences.FirstOrDefault(p => p.WeekDay == (WeekDay)dtNow.DayOfWeek);

                    // Caso não tenha, não precisa fazer nada
                    if (attendanceToday == null)
                    {
                        Log($"Cliente não possui atendimento hoje, pulando para o próximo.");
                        continue;
                    }

                    // Caso tenha, valida se já criou o atendimento
                    var attendanceDate = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                    var attendance = client.Attendances.FirstOrDefault(p => p.Date.Between(attendanceDate));

                    // Caso não tenha criado o atendimento, então cria
                    if (attendance == null)
                    {
                        Log($"Atendimento ainda não criado, fazendo a criação...");

                        attendance = new Attendance(client.ID, attendanceDate, attendanceToday.StartTime, attendanceToday.EndTime, string.Empty);
                        Repository.Create(attendance);
                    }

                    // Valida se está na hora de mandar o e-mail e se já mandou
                    var startDate = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, attendanceToday.StartTime.Hour, attendanceToday.StartTime.Minute, 0);

                    Log($"Fazendo validação se deve enviar o email. O atendimento foi enviado?: {attendance.AlertHasSend}. Hora do atendimento: {startDate.ToString("dd/MM/yyyy HH:mm:ss")}, e hora atual: {dtNow.ToString("dd/MM/yyyy HH:mm:ss")}");

                    if (!attendance.AlertHasSend && startDate.AddMinutes(-30) <= dtNow)
                    {
                        Log($"Enviando e-mail para {user.Mail}...");

                        attendance.SetAlertSended();
                        var emailHasSend = Email.Send(user.Mail, "Alerta de atendimento", BuildMailBody(user.FullName, client.Name, attendance.HourInitial));
                        Log($"Email enviado?: {emailHasSend}");
                    }
                }
            }

            await CommitAsync();

            return bool.TrueString;
        }

        private string BuildMailBody(string userFullame, string clientName, DateTime startAttendance)
        {
            var sb = new StringBuilder();

            sb.Append($"&nbsp&nbsp&nbsp&nbsp Olá {userFullame}");
            sb.AppendLine("<br>");
            sb.AppendLine($"<p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspO atendimento do paciente <b>{clientName}</b> ocorre às <b><u>{startAttendance.ToString("HH:mm")}</u></b>");
            sb.AppendLine("<br>");
            sb.AppendLine("<p>&nbsp&nbsp&nbsp&nbspAtt,");

            return sb.ToString();
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
