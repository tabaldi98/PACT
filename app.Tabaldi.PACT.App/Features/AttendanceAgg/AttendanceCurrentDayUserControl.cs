using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.AttendanceRecurrenceAgg;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceCurrentDayUserControl : UserControl
    {
        private readonly IAttendanceRecurrenceRepository _attendanceRepository;

        public AttendanceCurrentDayUserControl()
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

            dgAttendances.DataSource = await _attendanceRepository.RetrieveByTypeAsync<AttendancesCurrentDayModel>(ViewPeriodType.Today);

            dgAttendances.Columns[nameof(AttendancesCurrentDayModel.ClientName)].HeaderText = "Cliente";
            dgAttendances.Columns[nameof(AttendancesCurrentDayModel.StartAttendance)].HeaderText = "Hora de inicio";
            dgAttendances.Columns[nameof(AttendancesCurrentDayModel.StartAttendance)].DefaultCellStyle.Format = "HH:mm";
            dgAttendances.Columns[nameof(AttendancesCurrentDayModel.EndAttendance)].HeaderText = "Hora do final";
            dgAttendances.Columns[nameof(AttendancesCurrentDayModel.EndAttendance)].DefaultCellStyle.Format = "HH:mm";

            this.SetLoading(false);
        }
    }
}
