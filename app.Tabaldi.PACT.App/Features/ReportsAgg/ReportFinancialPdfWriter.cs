using app.Tabaldi.PACT.LibraryModels.ClientsModule.Models;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace app.Tabaldi.PACT.App.Features.ReportsAgg
{
    public static class ReportFinancialPdfWriter
    {
        public static void WriteFinancialReport(IEnumerable<ReportsFinancialModel> reportsModel, string fileName, DateTime initialDate, DateTime endDate)
        {
            var fileStream = new FileStream(fileName, FileMode.Create);

            var document = new Document(PageSize.A4, 40, 40, 40, 40);

            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.Add(new Paragraph("Relatório de finanças", FontFactory.GetFont("Segoe UI", 35f, BaseColor.BLACK))
            {
                Alignment = 1,
            });

            document.Add(new Chunk(string.Empty));

            var table = new PdfPTable(new float[] { 10f, 10f, 10f })
            {
                WidthPercentage = 100f,
            };
            table.AddCell(GetHeaderCell("Paciente"));
            table.AddCell(GetHeaderCell("Quantidade de atendimentos"));
            table.AddCell(GetHeaderCell("Valor total (R$)"));

            foreach (var model in reportsModel)
            {
                table.AddCell(GetCell(model.ClientName));
                table.AddCell(GetCell(model.TotalAttendances));
                table.AddCell(GetCell(model.TotalValue));
            }

            table.AddCell(GetCell("Total", useBoldFont: true));
            table.AddCell(GetCell(reportsModel.Sum(p => p.TotalAttendances), useBoldFont: true));
            table.AddCell(GetCell(reportsModel.Sum(p => p.TotalValue), useBoldFont: true));

            document.Add(table);

            document.Add(new Chunk(string.Empty));

            document.Add(new Paragraph($"Referência: de {initialDate.ToString("dd/MM/yyyy")} até {endDate.ToString("dd/MM/yyyy")}", FontFactory.GetFont("Segoe UI", 12f, BaseColor.BLACK))
            {
                Alignment = 0,
            });

            AddImageHeader(writer);

            document.Close();
            writer.Close();
            fileStream.Close();
        }

        public static void WriteAttendanceReport(IEnumerable<ReportsAttendancesModel> reportsModel, string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Create);

            var document = new Document(PageSize.A4, 40, 40, 40, 40);

            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.Add(new Paragraph($"Relatório de evolução do(a) {reportsModel.First().ClientName}", FontFactory.GetFont("Segoe UI", 25f, BaseColor.BLACK))
            {
                Alignment = 1,
            });

            document.Add(new Chunk(string.Empty));

            var table = new PdfPTable(new float[] { 10f, 5f, 5f, 30f })
            {
                WidthPercentage = 100f,
            };
            table.AddCell(GetHeaderCell("Dia do atendimento"));
            table.AddCell(GetHeaderCell("Hora inicial"));
            table.AddCell(GetHeaderCell("Hora final"));
            table.AddCell(GetHeaderCell("Evolução"));

            foreach (var model in reportsModel)
            {
                table.AddCell(GetCell(model.AttendanceDate.ToString("dd/MM/yyyy")));
                table.AddCell(GetCell(model.StartTime.ToString("HH:mm")));
                table.AddCell(GetCell(model.EndTime.ToString("HH:mm")));
                table.AddCell(GetCell(model.Description));
            }

            document.Add(table);

            document.Add(new Chunk(string.Empty));

            AddImageHeader(writer);

            document.Close();
            writer.Close();
            fileStream.Close();
        }

        public static void WriteClientReport(ClientModel clientModel, string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Create);

            var document = new Document(PageSize.A4, 40, 40, 40, 40);

            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.Add(new Paragraph($"Relatório do paciente {clientModel.Name}", FontFactory.GetFont("Segoe UI", 25f, BaseColor.BLACK))
            {
                Alignment = 1,
            });

            document.Add(new Chunk(string.Empty));

            document.Add(new Paragraph($"Data de nascimento: ", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"- {clientModel.DateOfBirth.ToString("dd/MM/yyyy")}", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.UNDEFINED))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"Telefone: ", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"- {clientModel.Phone}", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.UNDEFINED))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"Diagnóstico clínico: ", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"- {clientModel.ClinicalDiagnosis}", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.UNDEFINED))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"Diagnóstico fisioterapêutico: ", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"- {clientModel.PhysiotherapeuticDiagnosis}", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.UNDEFINED))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"Objetivos: ", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"- {clientModel.Objectives}", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.UNDEFINED))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"Conduta de tratamento: ", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD))
            {
                Alignment = 0,
            });

            document.Add(new Paragraph($"- {clientModel.TreatmentConduct}", new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.UNDEFINED))
            {
                Alignment = 0,
            });

            document.Add(new Chunk(string.Empty));

            AddImageHeader(writer);

            document.Close();
            writer.Close();
            fileStream.Close();
        }

        private static PdfPCell GetHeaderCell(string text)
        {
            return new PdfPCell(new Phrase(10f, text, new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD)))
            {
                BackgroundColor = new BaseColor(193, 205, 205),
                Colspan = 1,
                HorizontalAlignment = 1
            };
        }

        private static PdfPCell GetCell(object text, bool useBoldFont = false)
        {
            return new PdfPCell(new Phrase(10f, text.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 15f, useBoldFont ? Font.BOLD : Font.NORMAL)))
            {
                BackgroundColor = new BaseColor(224, 238, 238),
                HorizontalAlignment = 1,
            };
        }

        private static void AddImageHeader(PdfWriter writer)
        {
            var image = Image.GetInstance(@"Properties\\fundo_branco.icon.jpeg");
            image.SetAbsolutePosition(0, 0);
            image.ScaleAbsolute(200, 50);
            var directContent = writer.DirectContent;
            var template = directContent.CreateTemplate(350, 95);
            template.AddImage(image);
            directContent.AddTemplate(template, 0, 842 - 50);
        }
    }
}
