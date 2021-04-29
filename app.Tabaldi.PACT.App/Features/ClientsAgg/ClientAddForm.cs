using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.DependencyResolution;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Commands;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Models;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ClientsAgg
{
    public partial class ClientAddForm : Form
    {
        private readonly IClientClientRepository _clientRepository = AutofacConfig.Container.Value.Resolve<IClientClientRepository>();
        private int? _id;

        public ClientAddForm(ClientModel clientModel = null)
        {
            InitializeComponent();

            SetListDaysData();

            SetClientData(clientModel);

            txtName.Select();
        }

        #region Métodos privados

        private void SetListDaysData()
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
        }

        public void SetClientData(ClientModel clientModel = null)
        {
            if (clientModel != null)
            {
                Text = $"Atualizar paciente {clientModel.Name}";
                checkEnabled.Visible = true;
                checkEnabled.Checked = clientModel.Enabled;

                txtName.Text = clientModel.Name;
                txtPhone.Text = clientModel.Phone;
                txtDiagnosisClinical.Text = clientModel.ClinicalDiagnosis;
                txtObjective.Text = clientModel.Objectives;
                txtDiagnosisPhysiotherapeutic.Text = clientModel.PhysiotherapeuticDiagnosis;
                txtTreatmentConduct.Text = clientModel.TreatmentConduct;
                dateBt.Value = clientModel.DateOfBirth;
                _id = clientModel.ID;
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
      
        private bool ValidateAllFields()
        {
            if (txtName.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblName.Text);

                return false;
            }

            if (txtPhone.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblPhone.Text);

                return false;
            }

            if (dateBt.Value == null)
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblDtBt.Text);

                return false;
            }

            if (txtDiagnosisClinical.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblDiag.Text);

                return false;
            }

            if (txtObjective.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblObjective.Text);

                return false;
            }

            return true;
        }

        private ClientAddCommand GetAddCommand()
        {
            return new ClientAddCommand()
            {
                Name = txtName.Text,
                Phone = txtPhone.Text,
                DateOfBirth = dateBt.Value,
                ClinicalDiagnosis = txtDiagnosisClinical.Text,
                Objectives = txtObjective.Text,
                PhysiotherapeuticDiagnosis = txtDiagnosisPhysiotherapeutic.Text,
                TreatmentConduct = txtTreatmentConduct.Text,
                ChargingType = dayType.Checked ? ChargingType.Day : ChargingType.Month,
                Value = txtValue.Value,
                Recurrences = GetRecurrenceCommand(),
            };
        }

        private ClientEditCommand GetEditCommand()
        {
            return new ClientEditCommand()
            {
                ID = _id.Value,
                Name = txtName.Text,
                Phone = txtPhone.Text,
                DateOfBirth = dateBt.Value,
                ClinicalDiagnosis = txtDiagnosisClinical.Text,
                Objectives = txtObjective.Text,
                PhysiotherapeuticDiagnosis = txtDiagnosisPhysiotherapeutic.Text,
                TreatmentConduct = txtTreatmentConduct.Text,
                ChargingType = dayType.Checked ? ChargingType.Day : ChargingType.Month,
                Value = txtValue.Value,
                Recurrences = GetRecurrenceCommand(),
                Enabled = checkEnabled.Checked,
            };
        }

        private IEnumerable<AttendanceRecurrenceCommand> GetRecurrenceCommand()
        {
            foreach (var checkedWeekDay in GetCheckedItems())
            {
                yield return new AttendanceRecurrenceCommand()
                {
                    WeekDay = checkedWeekDay,
                    StartTime =
                         checkedWeekDay == WeekDay.Monday ? dtStartMonday.Value :
                         checkedWeekDay == WeekDay.Tuesday ? dtStartTuesday.Value :
                         checkedWeekDay == WeekDay.Wednesday ? dtStartWednesday.Value :
                         checkedWeekDay == WeekDay.Thursday ? dtStartThursday.Value :
                         checkedWeekDay == WeekDay.Friday ? dtStartFriday.Value :
                         checkedWeekDay == WeekDay.Saturday ? dtStartSaturday.Value :
                         checkedWeekDay == WeekDay.Sunday ? dtStartSunday.Value
                            : DateTime.Now,
                    EndTime =
                         checkedWeekDay == WeekDay.Monday ? dtEndMonday.Value :
                         checkedWeekDay == WeekDay.Tuesday ? dtEndTuesday.Value :
                         checkedWeekDay == WeekDay.Wednesday ? dtEndWednesday.Value :
                         checkedWeekDay == WeekDay.Thursday ? dtEndThursday.Value :
                         checkedWeekDay == WeekDay.Friday ? dtEndFriday.Value :
                         checkedWeekDay == WeekDay.Saturday ? dtEndSaturday.Value :
                         checkedWeekDay == WeekDay.Sunday ? dtEndSunday.Value
                            : DateTime.Now,
                };
            }
        }

        private IEnumerable<WeekDay> GetCheckedItems()
        {
            foreach (var selectedItem in listDaysAttendance.CheckedItems)
            {
                yield return ((KeyValuePair<WeekDay, string>)selectedItem).Key;
            }
        }

        #endregion

        #region Eventos

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateAllFields()) { return; }

            try
            {
                this.SetLoading(true);

                if (_id.HasValue)
                {
                    await _clientRepository.UpdateAsync(GetEditCommand());
                    MessageBoxExtensions.ShowSucessMessage("Paciente atualizado com sucesso");
                }
                else
                {
                    await _clientRepository.CreateAsync(GetAddCommand());
                    MessageBoxExtensions.ShowSucessMessage("Paciente adicionado com sucesso");
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


        private void btnCancel_Click(object sender, EventArgs e)
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

        #endregion
    }
}
