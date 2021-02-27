using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ClientsAgg
{
    public partial class ClientAddForm : Form
    {
        private readonly IClientClientRepository _clientRepository;
        private int? _id;

        public ClientAddForm(ClientModel clientModel = null)
        {
            InitializeComponent();

            _clientRepository = new ClientClientRepository();

            SetData(clientModel);
        }

        public void SetData(ClientModel clientModel = null)
        {
            listDaysAttendance.Items.Add("Segunda-feira");
            listDaysAttendance.Items.Add("Terça-feira");
            listDaysAttendance.Items.Add("Quarta-feira");
            listDaysAttendance.Items.Add("Quinta-feira");
            listDaysAttendance.Items.Add("Sexta-feira");
            listDaysAttendance.Items.Add("Sábado");
            listDaysAttendance.Items.Add("Domingo");

            if (clientModel != null)
            {
                txtName.Text = clientModel.Name;
                txtPhone.Text = clientModel.Phone;
                txtDiagnosis.Text = clientModel.Diagnosis;
                dateBt.Value = clientModel.DateOfBirth;
                _id = clientModel.ID;
                txtObjective.Text = clientModel.Objective;
                txtValue.Value = clientModel.Value;
                dayType.Checked = clientModel.ChargingType == ChargingType.Day;
                monthType.Checked = clientModel.ChargingType == ChargingType.Month;

                for (var count = 0; count < listDaysAttendance.Items.Count; count++)
                {
                    if (clientModel.DaysOffAttendance.Contains(listDaysAttendance.Items[count].ToString()))
                    {
                        listDaysAttendance.SetItemChecked(count, true);
                    }
                }

                var mondayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Segunda-feira"));
                dtStartMonday.Enabled = dtEndMonday.Enabled = mondayEnabled;
                dtStartMonday.Value = mondayEnabled ? clientModel.StartMonday.Value : dtStartMonday.Value;
                dtEndMonday.Value = mondayEnabled ? clientModel.EndMonday.Value : dtEndMonday.Value;

                var tuesdayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Terça-feira"));
                dtStartTuesday.Enabled = dtEndTuesday.Enabled = tuesdayEnabled;
                dtStartTuesday.Value = tuesdayEnabled ? clientModel.StartTuesday.Value : dtStartTuesday.Value;
                dtEndTuesday.Value = tuesdayEnabled ? clientModel.EndTuesday.Value : dtEndTuesday.Value;

                var wednesdayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Quarta-feira"));
                dtStartWednesday.Enabled = dtEndWednesday.Enabled = wednesdayEnabled;
                dtStartWednesday.Value = wednesdayEnabled ? clientModel.StartWednesday.Value : dtStartWednesday.Value;
                dtEndWednesday.Value = wednesdayEnabled ? clientModel.EndWednesday.Value : dtEndWednesday.Value;

                var thursdayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Quinta-feira"));
                dtStartThursday.Enabled = dtEndThursday.Enabled = thursdayEnabled;
                dtStartThursday.Value = thursdayEnabled ? clientModel.StartThursday.Value : dtStartThursday.Value;
                dtEndThursday.Value = thursdayEnabled ? clientModel.EndThursday.Value : dtEndThursday.Value;

                var fridayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Sexta-feira"));
                dtStartFriday.Enabled = dtEndFriday.Enabled = fridayEnabled;
                dtStartFriday.Value = fridayEnabled ? clientModel.StartFriday.Value : dtStartFriday.Value;
                dtEndFriday.Value = fridayEnabled ? clientModel.EndFriday.Value : dtEndFriday.Value;

                var saturdayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Sábado"));
                dtStartSaturday.Enabled = dtEndSaturday.Enabled = saturdayEnabled;
                dtStartSaturday.Value = saturdayEnabled ? clientModel.StartSaturday.Value : dtStartSaturday.Value;
                dtEndSaturday.Value = saturdayEnabled ? clientModel.EndSaturday.Value : dtEndSaturday.Value;

                var sundayEnabled = clientModel.DaysOffAttendance.Any(p => p.Equals("Domingo"));
                dtStartSunday.Enabled = dtEndSunday.Enabled = sundayEnabled;
                dtStartSunday.Value = sundayEnabled ? clientModel.StartSunday.Value : dtStartSunday.Value;
                dtEndSunday.Value = sundayEnabled ? clientModel.EndSunday.Value : dtEndSunday.Value;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateAllFields()) { return; }

            try
            {
                this.SetLoading(true);

                if (_id.HasValue)
                {
                    await _clientRepository.UpdateAsync(GetEditCommand());
                    MessageBoxExtensions.ShowSucessMessage("Cliente atualizado com sucesso");
                }
                else
                {
                    await _clientRepository.CreateAsync(GetAddCommand());
                    MessageBoxExtensions.ShowSucessMessage("Cliente adicionado com sucesso");
                }

                Close();
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
            if (txtName.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldsMessage(lblName.Text);

                return false;
            }

            if (txtPhone.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldsMessage(lblPhone.Text);

                return false;
            }

            if (dateBt.Value == null)
            {
                MessageBoxExtensions.ShowEmptyFieldsMessage(lblDtBt.Text);

                return false;
            }

            if (txtDiagnosis.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldsMessage(lblDiag.Text);

                return false;
            }

            if (txtObjective.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldsMessage(lblObjective.Text);

                return false;
            }

            return true;
        }

        public ClientAddCommand GetAddCommand()
        {
            return new ClientAddCommand()
            {
                Name = txtName.Text,
                Phone = txtPhone.Text,
                DateOfBirth = dateBt.Value,
                Diagnosis = txtDiagnosis.Text,
                Objective = txtObjective.Text,
                DaysOffAttendance = GetCheckedItems(),
                ChargingType = dayType.Checked ? ChargingType.Day : ChargingType.Month,
                Value = txtValue.Value,
                StartMonday = dtStartMonday.Value,
                EndMonday = dtEndMonday.Value,
                StartTuesday = dtStartTuesday.Value,
                EndTuesday = dtEndTuesday.Value,
                StartWednesday = dtStartWednesday.Value,
                EndWednesday = dtEndWednesday.Value,
                StartThursday = dtStartThursday.Value,
                EndThursday = dtEndThursday.Value,
                StartFriday = dtStartFriday.Value,
                EndFriday = dtEndFriday.Value,
                StartSaturday = dtStartSaturday.Value,
                EndSaturday = dtEndSaturday.Value,
                StartSunday = dtStartSunday.Value,
                EndSunday = dtEndSunday.Value,
            };
        }

        public ClientEditCommand GetEditCommand()
        {
            return new ClientEditCommand()
            {
                ID = _id.Value,
                Name = txtName.Text,
                Phone = txtPhone.Text,
                DateOfBirth = dateBt.Value,
                Diagnosis = txtDiagnosis.Text,
                Objective = txtObjective.Text,
                DaysOffAttendance = GetCheckedItems(),
                ChargingType = dayType.Checked ? ChargingType.Day : ChargingType.Month,
                Value = txtValue.Value,
                StartMonday = dtStartMonday.Value,
                EndMonday = dtEndMonday.Value,
                StartTuesday = dtEndTuesday.Value,
                EndTuesday = dtEndTuesday.Value,
                StartWednesday = dtEndWednesday.Value,
                EndWednesday = dtEndWednesday.Value,
                StartThursday = dtEndThursday.Value,
                EndThursday = dtEndThursday.Value,
                StartFriday = dtEndFriday.Value,
                EndFriday = dtEndFriday.Value,
                StartSaturday = dtEndSaturday.Value,
                EndSaturday = dtEndSaturday.Value,
                StartSunday = dtEndSunday.Value,
                EndSunday = dtEndSunday.Value,
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void listDaysAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = GetCheckedItems();

            var mondayEnabled = selectedItems.Any(p => p.Equals("Segunda-feira"));
            dtStartMonday.Enabled = dtEndMonday.Enabled = mondayEnabled;

            var tuesdayEnabled = selectedItems.Any(p => p.Equals("Terça-feira"));
            dtStartTuesday.Enabled = dtEndTuesday.Enabled = tuesdayEnabled;

            var wednesdayEnabled = selectedItems.Any(p => p.Equals("Quarta-feira"));
            dtStartWednesday.Enabled = dtEndWednesday.Enabled = wednesdayEnabled;

            var thursdayEnabled = selectedItems.Any(p => p.Equals("Quinta-feira"));
            dtStartThursday.Enabled = dtEndThursday.Enabled = thursdayEnabled;

            var fridayEnabled = selectedItems.Any(p => p.Equals("Sexta-feira"));
            dtStartFriday.Enabled = dtEndFriday.Enabled = fridayEnabled;

            var saturdayEnabled = selectedItems.Any(p => p.Equals("Sábado"));
            dtStartSaturday.Enabled = dtEndSaturday.Enabled = saturdayEnabled;

            var sundayEnabled = selectedItems.Any(p => p.Equals("Domingo"));
            dtStartSunday.Enabled = dtEndSunday.Enabled = sundayEnabled;
        }

        private IEnumerable<string> GetCheckedItems()
        {
            foreach (var selectedItem in listDaysAttendance.CheckedItems)
            {
                yield return selectedItem.ToString();
            }
        }
    }
}
