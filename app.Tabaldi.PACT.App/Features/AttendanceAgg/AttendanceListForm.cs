using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceListForm : Form
    {
        private readonly ClientModel _clientModel;
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceListForm(ClientModel clientModel)
        {
            InitializeComponent();

            _clientModel = clientModel;
            _attendanceRepository = new AttendanceRepository();
        }

        private async void SetData(int? month = null)
        {
            this.SetLoading(true);

            var list = await _attendanceRepository.GetAllAsync(_clientModel.ID);

            if (month != null && month.Value != 0)
            {
                dgAttendances.DataSource = list.Where(p => p.Date.Month == month.Value).OrderByDescending(p => p.Date).ToList();
            }
            else
            {
                dgAttendances.DataSource = list.OrderByDescending(p => p.Date).ToList();
            }

            dgAttendances.Columns[nameof(AttendanceModel.ID)].Visible = false;

            dgAttendances.Columns[nameof(AttendanceModel.Date)].HeaderText = "Data";

            dgAttendances.Columns[nameof(AttendanceModel.Date)].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgAttendances.Columns[nameof(AttendanceModel.HourInitial)].HeaderText = "Hora Inicial";
            dgAttendances.Columns[nameof(AttendanceModel.HourInitial)].DefaultCellStyle.Format = "HH:mm";

            dgAttendances.Columns[nameof(AttendanceModel.HourFinish)].HeaderText = "Hora Final";
            dgAttendances.Columns[nameof(AttendanceModel.HourFinish)].DefaultCellStyle.Format = "HH:mm";

            dgAttendances.Columns[nameof(AttendanceModel.Description)].HeaderText = "Descrição";

            this.SetLoading(false);
        }

        private void AttendanceListForm_Load(object sender, System.EventArgs e)
        {
            txtClientName.Text = _clientModel.Name;
            dgAttendances.ReadOnly = true;
            dgAttendances.AutoGenerateColumns = true;

            SetData();

            dropMonth.Items.Add(Month.Todos);
            dropMonth.Items.Add(Month.Janeiro);
            dropMonth.Items.Add(Month.Fevereiro);
            dropMonth.Items.Add(Month.Março);
            dropMonth.Items.Add(Month.Abril);
            dropMonth.Items.Add(Month.Maio);
            dropMonth.Items.Add(Month.Junho);
            dropMonth.Items.Add(Month.Julho);
            dropMonth.Items.Add(Month.Agosto);
            dropMonth.Items.Add(Month.Setembro);
            dropMonth.Items.Add(Month.Outubro);
            dropMonth.Items.Add(Month.Novembro);
            dropMonth.Items.Add(Month.Dezembro);

            dropMonth.SelectedItem = Month.Todos;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            new AttendanceAddForm(_clientModel).ShowDialog();

            SetData();
        }

        private void dgAttendances_SelectionChanged(object sender, System.EventArgs e)
        {
            if (GetSelecteds().Count() == 1)
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
            }
        }

        public IEnumerable<AttendanceModel> GetSelecteds()
        {
            foreach (DataGridViewRow selected in dgAttendances.SelectedRows)
            {
                yield return selected.DataBoundItem as AttendanceModel;
            }
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            new AttendanceAddForm(_clientModel, GetSelecteds().FirstOrDefault()).ShowDialog();

            SetData();
        }

        private void dropMonth_SelectedValueChanged(object sender, System.EventArgs e)
        {
            var selectedItem = (Month)dropMonth.SelectedItem;

            SetData((int)selectedItem);
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var selecteds = GetSelecteds();

            if (MessageBoxExtensions.ShowDeleteQuestionMessage("Atendimentos", selecteds.Select(p => p.Description).ToList()) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.SetLoading(false);
                var command = new AttendanceRemoveCommand();
                command.SetIds(selecteds.Select(p => p.ID).ToArray());
                await _attendanceRepository.DeleteAsync(command);

                MessageBoxExtensions.ShowSucessMessage("Atendimentos(s) deletado(s) com sucesso");
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
    }

    public enum Month
    {
        Todos,
        Janeiro = 1, //Janeiro
        Fevereiro = 2, // Fevereiro
        Março = 3, // Março
        Abril = 4, // Abril
        Maio = 5, // Maio
        Junho = 6, //Junho
        Julho = 7, // Julho
        Agosto = 8, // Agosto
        Setembro = 9, // Setembro
        Outubro = 10, // Outubro
        Novembro = 11, // Novembro
        Dezembro = 12, //Dezembro
    }
}
