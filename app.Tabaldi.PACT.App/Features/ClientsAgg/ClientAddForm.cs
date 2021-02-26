using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using System;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ClientsAgg
{
    public partial class ClientAddForm : Form
    {
        private readonly IClientClientRepository _clientRepository;
        private readonly int? _id;

        public ClientAddForm(ClientModel clientModel = null)
        {
            InitializeComponent();

            _clientRepository = new ClientClientRepository();

            if (clientModel != null)
            {
                txtName.Text = clientModel.Name;
                txtPhone.Text = clientModel.Phone;
                txtDiagnosis.Text = clientModel.Diagnosis;
                dateBt.Value = clientModel.DateOfBirth;
                _id = clientModel.ID;
                txtObjective.Text = clientModel.Objective;
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
                DateOfBirth = dateBt.Value,
                Diagnosis = txtDiagnosis.Text,
                Phone = txtPhone.Text,
                Objective = txtObjective.Text,
            };
        }

        public ClientEditCommand GetEditCommand()
        {
            return new ClientEditCommand()
            {
                ID = _id.Value,
                Name = txtName.Text,
                DateOfBirth = dateBt.Value,
                Diagnosis = txtDiagnosis.Text,
                Phone = txtPhone.Text,
                Objective = txtObjective.Text
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
