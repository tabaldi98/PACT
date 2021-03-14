using app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands;
using System;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ReportsAgg
{
    public partial class ReportsDefaultFilterForm : Form
    {
        public ReportsDefaultFilterCommand Command { get; private set; }

        public ReportsDefaultFilterForm()
        {
            InitializeComponent();

            var dtNow = DateTime.Now;
            initialDate.Value = new DateTime(dtNow.Year, dtNow.Month, 1);
            endDate.Value = initialDate.Value.AddMonths(1).AddDays(-1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Command = new ReportsDefaultFilterCommand
            {
                InitialDate = initialDate.Value,
                EndDate = endDate.Value,
            };

            DialogResult = DialogResult.OK;
        }
    }
}
