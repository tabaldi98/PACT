using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.Features.ClientsAgg;
using app.Tabaldi.PACT.Infra.Data.HttpClient.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceCurrentWeekUserControl : UserControl
    {
        private readonly IAttendanceRecurrenceRepository _attendanceRepository;
        private readonly IClientClientRepository _clientRepository;

        public AttendanceCurrentWeekUserControl()
        {
            InitializeComponent();

            dgAttendances.AutoGenerateColumns = true;
            dgAttendances.ReadOnly = true;

            _attendanceRepository = new AttendanceRecurrenceRepository();
            _clientRepository = new ClientClientRepository();

            SetData();
        }

        public async void SetData()
        {
            this.SetLoading(true);

            dgAttendances.DataSource = await _attendanceRepository.RetrieveByTypeAsync<AttendancesCurrentWeekModel>(ViewPeriodType.CurrentWeek);

            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.ClientID)].Visible = false;
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.ClientName)].HeaderText = "Paciente";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.DayOfAttendance)].HeaderText = "Quando";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.StartAttendance)].HeaderText = "Hora de inicio";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.StartAttendance)].DefaultCellStyle.Format = "HH:mm";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.EndAttendance)].HeaderText = "Hora do final";
            dgAttendances.Columns[nameof(AttendancesCurrentWeekModel.EndAttendance)].DefaultCellStyle.Format = "HH:mm";

            this.SetLoading(false);
        }

        private async void dgAttendances_DoubleClick(object sender, System.EventArgs e)
        {
            this.SetIsLoading();

            var clientModel = await _clientRepository.GetByIdAsync(GetSelecteds().FirstOrDefault().ClientID);

            this.SetNoLoading();

            new ClientAddForm(clientModel).ShowDialog();
        }

        public IEnumerable<AttendancesCurrentWeekModel> GetSelecteds()
        {
            foreach (DataGridViewRow selected in dgAttendances.SelectedRows)
            {
                yield return selected.DataBoundItem as AttendancesCurrentWeekModel;
            }
        }
    }
}
