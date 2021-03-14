using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.DependencyResolution;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ReportsModule;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands;
using Autofac;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ReportsAgg
{
    public partial class ReportsUserControl : UserControl
    {
        public readonly IReportRepository _reportRepository = AutofacConfig.Container.Value.Resolve<IReportRepository>();

        public ReportsUserControl()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var form = new ReportsDefaultFilterForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.SetIsLoading();

                    var report = await _reportRepository.RetrieveFinancialReportAsync(form.Command);

                    if (!report.Any())
                    {
                        MessageBox.Show("Nenhum dado no período informado");
                        return;
                    }

                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Arquivo PDF (*.PDF) | *.PDF",
                        RestoreDirectory = true,
                        FileName = $"Relatório_Financeiro_de_{form.Command.InitialDate.ToString("dd-MM-yyyy")}_até_{form.Command.EndDate.ToString("dd-MM-yyyy")}.PDF",
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ReportFinancialPdfWriter.WriteFinancialReport(report, saveFileDialog.FileName, form.Command.InitialDate, form.Command.EndDate);

                        Process.Start(saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                { }
                finally
                {
                    this.SetNoLoading();
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var form = new ReportSelectClientIDForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.SetIsLoading();

                    var report = await _reportRepository.RetrieveAttendanceReportAsync(new ReportClientIDFilterCommand() { ClientID = form.ClientID });

                    if (!report.Any())
                    {
                        MessageBox.Show("Nenhum dado para este cliente");
                        return;
                    }

                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Arquivo PDF (*.PDF) | *.PDF",
                        RestoreDirectory = true,
                        FileName = $"Relatório_Atendimentos_{report.First().ClientName}.PDF",
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ReportFinancialPdfWriter.WriteAttendanceReport(report, saveFileDialog.FileName);

                        Process.Start(saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                { }
                finally
                {
                    this.SetNoLoading();
                }
            }

        }
    }
}
