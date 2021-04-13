using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.DependencyResolution;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Models;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands;
using Autofac;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ReportsAgg
{
    public partial class ReportSelectClientIDForm : Form
    {
        public ReportClientIDFilterCommand Command { get; private set; }

        private readonly IClientClientRepository _clientClientRepository = AutofacConfig.Container.Value.Resolve<IClientClientRepository>();

        public ReportSelectClientIDForm(bool showDateFilter = true)
        {
            InitializeComponent();

            Enabled = true;

            groupFilterData.Visible = showDateFilter;

            var dtNow = DateTime.Now;
            startDate.Value = new DateTime(dtNow.Year, dtNow.Month, 1);
            endDate.Value = startDate.Value.AddMonths(1).AddDays(-1);
        }

        private async void SetData(string filter = null)
        {
            this.SetLoading(true);

            var list = await _clientClientRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
            }

            dgClients.DataSource = list;

            dgClients.Columns[nameof(ClientModel.ID)].Visible = false;
            dgClients.Columns[nameof(ClientModel.ChargingType)].Visible = false;
            dgClients.Columns[nameof(ClientModel.Value)].Visible = false;
            dgClients.Columns[nameof(ClientModel.Recurrences)].Visible = false;
            dgClients.Columns[nameof(ClientModel.RegistrationDate)].Visible = false;
            dgClients.Columns[nameof(ClientModel.PhysiotherapeuticDiagnosis)].Visible = false;
            dgClients.Columns[nameof(ClientModel.Objectives)].Visible = false;
            dgClients.Columns[nameof(ClientModel.TreatmentConduct)].Visible = false;
            dgClients.Columns[nameof(ClientModel.ClinicalDiagnosis)].Visible = false;

            dgClients.Columns[nameof(ClientModel.Name)].HeaderText = "Nome";
            dgClients.Columns[nameof(ClientModel.Phone)].HeaderText = "Telefone";
            dgClients.Columns[nameof(ClientModel.DateOfBirth)].HeaderText = "Data de nascimento";
            dgClients.Columns[nameof(ClientModel.DateOfBirth)].DefaultCellStyle.Format = "dd/MM/yyyy";

            this.SetLoading(false);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetData(txtFilter.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetData(txtFilter.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var selecteds = GetSelecteds();
            if (!selecteds.Any())
            {
                MessageBoxExtensions.ShowErrorMessage("Selecione um paciente");
                return;
            }

            Command = new ReportClientIDFilterCommand()
            {
                ClientID = selecteds.SingleOrDefault().ID,
                UseFilter = !checkAll.Checked,
                EndDate = endDate.Value,
                StartDate = startDate.Value,
            };

            DialogResult = DialogResult.OK;
        }

        public IEnumerable<ClientModel> GetSelecteds()
        {
            foreach (DataGridViewRow selected in dgClients.SelectedRows)
            {
                yield return selected.DataBoundItem as ClientModel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ReportSelectClientIDForm_Load(object sender, EventArgs e)
        {
            SetData();
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            startDate.Enabled = endDate.Enabled = !checkAll.Checked;
        }
    }
}
