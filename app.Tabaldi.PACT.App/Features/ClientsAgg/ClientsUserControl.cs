using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.Features.AttendanceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;

namespace app.Tabaldi.PACT.App.Features.ClientsAgg
{
    public partial class ClientsUserControl : UserControl
    {
        private readonly IClientClientRepository _clientClientRepository;
        public ClientsUserControl()
        {
            InitializeComponent();
            dgClients.AutoGenerateColumns = true;
            dgClients.ReadOnly = true;

            _clientClientRepository = new ClientClientRepository();
            SetData();
        }

        private async void SetData(string filter = null)
        {
            this.SetLoading(true);

            var list = await _clientClientRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(p => p.Name.Contains(filter)).ToList();
            }

            dgClients.DataSource = list;

            dgClients.Columns[nameof(ClientModel.ID)].Visible = false;
            dgClients.Columns[nameof(ClientModel.ChargingType)].Visible = false;
            dgClients.Columns[nameof(ClientModel.Value)].Visible = false;
            dgClients.Columns[nameof(ClientModel.Recurrences)].Visible = false;
            dgClients.Columns[nameof(ClientModel.RegistrationDate)].Visible = false;

            dgClients.Columns[nameof(ClientModel.Name)].HeaderText = "Nome";

            dgClients.Columns[nameof(ClientModel.Phone)].HeaderText = "Telefone";
            dgClients.Columns[nameof(ClientModel.DateOfBirth)].HeaderText = "Data de nascimento";
            dgClients.Columns[nameof(ClientModel.DateOfBirth)].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgClients.Columns[nameof(ClientModel.Diagnosis)].HeaderText = "Diagnóstico";
            dgClients.Columns[nameof(ClientModel.Objective)].HeaderText = "Tratamento/Objetivos";

            this.SetLoading(false);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var selecteds = GetSelecteds();

            if (MessageBoxExtensions.ShowDeleteQuestionMessage("Clientes", selecteds.Select(p => p.Name).ToList()) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.SetLoading(false);
                var command = new ClientRemoveCommand();
                command.SetIds(selecteds.Select(p => p.ID).ToArray());
                await _clientClientRepository.DeleteAsync(command);

                MessageBoxExtensions.ShowSucessMessage("Cliente(s) deletado(s) com sucesso");
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowErrorMessage(ex);
            }
            finally
            {
                this.SetLoading(false);

                SetData();
            }
        }

        public IEnumerable<ClientModel> GetSelecteds()
        {
            foreach (DataGridViewRow selected in dgClients.SelectedRows)
            {
                yield return selected.DataBoundItem as ClientModel;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ClientAddForm().ShowDialog();

            SetData();
        }

        private void dgClients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgClients.SelectedRows.Count == 1)
            {
                btnEdit.Enabled = true;
                btnAttendance.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnAttendance.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            new ClientAddForm(GetSelecteds().FirstOrDefault()).ShowDialog();

            SetData();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetData(txtFilter.Text);
                txtFilter.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetData(txtFilter.Text);

            txtFilter.Clear();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            new AttendanceListForm(GetSelecteds().FirstOrDefault()).ShowDialog();
        }

        private void dgClients_DoubleClick(object sender, EventArgs e)
        {
            new ClientAddForm(GetSelecteds().FirstOrDefault()).ShowDialog();

            SetData();
        }
    }
}
