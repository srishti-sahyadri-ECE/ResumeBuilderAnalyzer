using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ResumeBuilderAnalyzer.Models;

namespace ResumeBuilderAnalyzer.Services
{
    public class PdfService
    {
        public byte[] GenerateResumePDF(Resume resume)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Content().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span("Resume").FontSize(24).Bold().Underline();
                        });

                        column.Item().PaddingTop(10).Text($"Full Name: {resume.FullName}");
                        column.Item().Text($"Email: {resume.Email}");
                        column.Item().Text($"Phone: {resume.Phone}");

                        column.Item().PaddingTop(10).Text("Skills").Bold();
                        column.Item().Text(resume.Skills ?? "N/A");

                        column.Item().PaddingTop(10).Text("Experience").Bold();
                        column.Item().Text(resume.Experience ?? "N/A");

                        column.Item().PaddingTop(10).Text("Education").Bold();
                        column.Item().Text(resume.Education ?? "N/A");
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
