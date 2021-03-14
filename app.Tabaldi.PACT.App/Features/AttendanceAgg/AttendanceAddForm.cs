using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using System;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceAddForm : Form
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ClientModel _clientModel;
        private readonly AttendanceModel _attendanceModel;

        public AttendanceAddForm(ClientModel clientModel, AttendanceModel attendanceModel = null)
        {
            InitializeComponent();

            _attendanceRepository = new AttendanceRepository();
            _clientModel = clientModel;
            _attendanceModel = attendanceModel;
        }

        private void AttendanceAddForm_Load(object sender, EventArgs e)
        {
            initialHour.Value = DateTime.Now.AddHours(-1);

            if (_attendanceModel != null)
            {
                txtDescription.Text = _attendanceModel.Description;
                dtDay.Value = _attendanceModel.Date;
                initialHour.Value = _attendanceModel.HourInitial;
                finishHour.Value = _attendanceModel.HourFinish;

                txtDescription.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateAllFields()) { return; }

            try
            {
                this.SetLoading(true);

                if (_attendanceModel == null)
                {
                    await _attendanceRepository.CreateAsync(GetAddCommand());
                    MessageBoxExtensions.ShowSucessMessage("Atendimento adicionado com sucesso");
                }
                else
                {
                    await _attendanceRepository.UpdateAsync(GetEditCommand());
                    MessageBoxExtensions.ShowSucessMessage("Atendimento atualizado com sucesso");
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowErrorMessage(ex);
            }
            finally
            {
                this.SetLoading(false);
            }
        }

        private bool ValidateAllFields()
        {
            if (initialHour.Value > finishHour.Value)
            {
                MessageBoxExtensions.ShowEmptyFieldMessage("A hora inicial não pode ser maior que a hora final");

                return false;
            }

            return true;
        }

        private AttendanceAddCommand GetAddCommand()
        {
            return new AttendanceAddCommand
            {
                ClientID = _clientModel.ID,
                Date = dtDay.Value,
                HourInitial = initialHour.Value,
                HourFinish = finishHour.Value,
                Description = txtDescription.Text,
            };
        }

        private AttendanceEditCommand GetEditCommand()
        {
            return new AttendanceEditCommand
            {
                ID = _attendanceModel.ID,
                Date = dtDay.Value,
                HourInitial = initialHour.Value,
                HourFinish = finishHour.Value,
                Description = txtDescription.Text,
            };
        }
    }
}
