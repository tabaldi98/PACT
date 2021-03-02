using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.AttendanceRecurrenceAgg;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceCurrentWeekUserControl : UserControl
    {
        private readonly IAttendanceRecurrenceRepository _attendanceRepository;

        public AttendanceCurrentWeekUserControl()
        {
            InitializeComponent();

            dgAttendances.AutoGenerateColumns = true;
            dgAttendances.ReadOnly = true;

            _attendanceRepository = new AttendanceRecurrenceRepository();

            SetData();
        }

        public async void SetData()
        {
            this.SetLoading(true);

            dgAttendances.DataSource = await _attendanceRepository.RetrieveByTypeAsync<AttendancesCurrentWeekModel>(ViewPeriodType.CurrentWeek);

            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.ClientName)].HeaderText = "Cliente";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.DayOfAttendance)].HeaderText = "Quando";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.StartAttendance)].HeaderText = "Hora de inicio";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.StartAttendance)].DefaultCellStyle.Format = "HH:mm";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.EndAttendance)].HeaderText = "Hora do final";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.EndAttendance)].DefaultCellStyle.Format = "HH:mm";

            this.SetLoading(false);
        }
    }
}
