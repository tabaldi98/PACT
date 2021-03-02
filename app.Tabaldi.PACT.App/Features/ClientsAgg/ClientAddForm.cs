using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Commands;
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
            var periods = new Dictionary<WeekDay, string>
            {
                { WeekDay.Monday, "Segunda-feira" },
                { WeekDay.Tuesday, "Terça-feira" },
                { WeekDay.Wednesday, "Quarta-feira" },
                { WeekDay.Thursday, "Quinta-feira" },
                { WeekDay.Friday, "Sexa-feira" },
                { WeekDay.Saturday, "Sábado" },
                { WeekDay.Sunday, "Domingo" },
            };

            ((ListBox)listDaysAttendance).DataSource = new BindingSource(periods, null);
            ((ListBox)listDaysAttendance).DisplayMember = "Value";
            ((ListBox)listDaysAttendance).ValueMember = "Key";

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
                    if (clientModel.Recurrences.Any(p => p.WeekDay == ((KeyValuePair<WeekDay, string>)listDaysAttendance.Items[count]).Key))
                    {
                        listDaysAttendance.SetItemChecked(count, true);
                    }
                }

                var mondayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Monday);
                var mondayEnabled = mondayRecurrence != null;
                dtStartMonday.Enabled = dtEndMonday.Enabled = mondayEnabled;
                dtStartMonday.Value = mondayEnabled ? mondayRecurrence.StartTime : dtStartMonday.Value;
                dtEndMonday.Value = mondayEnabled ? mondayRecurrence.EndTime : dtEndMonday.Value;

                var tuesdayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Tuesday);
                var tuesdayEnabled = tuesdayRecurrence != null;
                dtStartTuesday.Enabled = dtEndTuesday.Enabled = tuesdayEnabled;
                dtStartTuesday.Value = tuesdayEnabled ? tuesdayRecurrence.StartTime : dtStartTuesday.Value;
                dtEndTuesday.Value = tuesdayEnabled ? tuesdayRecurrence.EndTime : dtEndTuesday.Value;

                var wednesdayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Wednesday);
                var wednesdayEnabled = wednesdayRecurrence != null;
                dtStartWednesday.Enabled = dtEndWednesday.Enabled = wednesdayEnabled;
                dtStartWednesday.Value = wednesdayEnabled ? wednesdayRecurrence.StartTime : dtStartWednesday.Value;
                dtEndWednesday.Value = wednesdayEnabled ? wednesdayRecurrence.EndTime : dtEndWednesday.Value;

                var thursdayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Thursday);
                var thursdayEnabled = thursdayRecurrence != null;
                dtStartThursday.Enabled = dtEndThursday.Enabled = thursdayEnabled;
                dtStartThursday.Value = thursdayEnabled ? thursdayRecurrence.StartTime : dtStartThursday.Value;
                dtEndThursday.Value = thursdayEnabled ? thursdayRecurrence.EndTime : dtEndThursday.Value;

                var fridayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Friday);
                var fridayEnabled = fridayRecurrence != null;
                dtStartFriday.Enabled = dtEndFriday.Enabled = fridayEnabled;
                dtStartFriday.Value = fridayEnabled ? fridayRecurrence.StartTime : dtStartFriday.Value;
                dtEndFriday.Value = fridayEnabled ? fridayRecurrence.EndTime : dtEndFriday.Value;

                var saturdayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Saturday);
                var saturdayEnabled = saturdayRecurrence != null;
                dtStartSaturday.Enabled = dtEndSaturday.Enabled = saturdayEnabled;
                dtStartSaturday.Value = saturdayEnabled ? saturdayRecurrence.StartTime : dtStartSaturday.Value;
                dtEndSaturday.Value = saturdayEnabled ? saturdayRecurrence.EndTime : dtEndSaturday.Value;

                var sundayRecurrence = clientModel.Recurrences.SingleOrDefault(p => p.WeekDay == WeekDay.Sunday);
                var sundayEnabled = sundayRecurrence != null;
                dtStartSunday.Enabled = dtEndSunday.Enabled = sundayEnabled;
                dtStartSunday.Value = sundayEnabled ? sundayRecurrence.StartTime : dtStartSunday.Value;
                dtEndSunday.Value = sundayEnabled ? sundayRecurrence.EndTime : dtEndSunday.Value;
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
                ChargingType = dayType.Checked ? ChargingType.Day : ChargingType.Month,
                Value = txtValue.Value,
                Recurrences = GetRecurrenceCommand(),
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
                ChargingType = dayType.Checked ? ChargingType.Day : ChargingType.Month,
                Value = txtValue.Value,
                Recurrences = GetRecurrenceCommand(),
            };
        }

        private IEnumerable<AttendanceRecurrenceCommand> GetRecurrenceCommand()
        {
            foreach (var item in GetCheckedItems())
            {
                yield return new AttendanceRecurrenceCommand()
                {
                    WeekDay = item,
                    StartTime =
                         item == WeekDay.Monday ? dtStartMonday.Value :
                         item == WeekDay.Tuesday ? dtStartTuesday.Value :
                         item == WeekDay.Wednesday ? dtStartWednesday.Value :
                         item == WeekDay.Thursday ? dtStartThursday.Value :
                         item == WeekDay.Friday ? dtStartFriday.Value :
                         item == WeekDay.Saturday ? dtStartSaturday.Value :
                         item == WeekDay.Sunday ? dtStartSunday.Value
                         : DateTime.Now,
                    EndTime =
                         item == WeekDay.Monday ? dtEndMonday.Value :
                         item == WeekDay.Tuesday ? dtEndTuesday.Value :
                         item == WeekDay.Wednesday ? dtEndWednesday.Value :
                         item == WeekDay.Thursday ? dtEndThursday.Value :
                         item == WeekDay.Friday ? dtEndFriday.Value :
                         item == WeekDay.Saturday ? dtEndSaturday.Value :
                         item == WeekDay.Sunday ? dtEndSunday.Value
                         : DateTime.Now,
                };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void listDaysAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = GetCheckedItems();

            var mondayEnabled = selectedItems.Any(p => p == WeekDay.Monday);
            dtStartMonday.Enabled = dtEndMonday.Enabled = mondayEnabled;

            var tuesdayEnabled = selectedItems.Any(p => p == WeekDay.Tuesday);
            dtStartTuesday.Enabled = dtEndTuesday.Enabled = tuesdayEnabled;

            var wednesdayEnabled = selectedItems.Any(p => p == WeekDay.Wednesday);
            dtStartWednesday.Enabled = dtEndWednesday.Enabled = wednesdayEnabled;

            var thursdayEnabled = selectedItems.Any(p => p == WeekDay.Thursday);
            dtStartThursday.Enabled = dtEndThursday.Enabled = thursdayEnabled;

            var fridayEnabled = selectedItems.Any(p => p == WeekDay.Friday);
            dtStartFriday.Enabled = dtEndFriday.Enabled = fridayEnabled;

            var saturdayEnabled = selectedItems.Any(p => p == WeekDay.Saturday);
            dtStartSaturday.Enabled = dtEndSaturday.Enabled = saturdayEnabled;

            var sundayEnabled = selectedItems.Any(p => p == WeekDay.Sunday);
            dtStartSunday.Enabled = dtEndSunday.Enabled = sundayEnabled;
        }

        private IEnumerable<WeekDay> GetCheckedItems()
        {
            foreach (var selectedItem in listDaysAttendance.CheckedItems)
            {
                yield return ((KeyValuePair<WeekDay, string>)selectedItem).Key;
            }
        }
    }
}
