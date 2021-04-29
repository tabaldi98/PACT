using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.DependencyResolution;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using app.Tabaldi.PACT.Infra.Data.HttpClient.ReportsModule;
using Autofac;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.ReportsAgg
{
    public partial class ReportsUserControl : UserControl
    {
        public readonly IReportRepository _reportRepository = AutofacConfig.Container.Value.Resolve<IReportRepository>();
        private readonly IClientClientRepository _clientClientRepository = AutofacConfig.Container.Value.Resolve<IClientClientRepository>();

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

                    var report = await _reportRepository.RetrieveAttendanceReportAsync(form.Command);

                    if (!report.Any())
                    {
                        MessageBox.Show("Nenhum dado para este paciente");
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
                catch
                { }
                finally
                {
                    this.SetNoLoading();
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var form = new ReportSelectClientIDForm(false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.SetIsLoading();

                    var client = await _clientClientRepository.GetByIdAsync(form.Command.ClientID);

                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Arquivo PDF (*.PDF) | *.PDF",
                        RestoreDirectory = true,
                        FileName = $"Relatório_Paciente_{client.Name}.PDF",
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ReportFinancialPdfWriter.WriteClientReport(client, saveFileDialog.FileName);

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
