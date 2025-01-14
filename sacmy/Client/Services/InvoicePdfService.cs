using sacmy.Shared.ViewModels.InvoiceViewModel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Layout.Borders;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using System.IO;
using Microsoft.JSInterop;
using System.Net.Http;

namespace sacmy.Client.Services
{
    public class InvoicePdfService : IDisposable
    {
        private PdfFont _boldFont;
        private PdfFont _regularFont;

        public InvoicePdfService()
        {
            try
            {
                string fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts");
                // Use Arial font which has good Arabic support
                _regularFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(fontPath, "arial.ttf"), PdfEncodings.IDENTITY_H);
                _boldFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(fontPath, "arialbd.ttf"), PdfEncodings.IDENTITY_H);
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing PDF fonts. Please ensure Arial font is installed on the system.", ex);
            }
        }

        public async Task<byte[]> GenerateInvoicePdf(BuyFatoraViewModel invoice, List<InvoiceItemsViewModel> items)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice), "Invoice data is required");

            if (items == null)
                throw new ArgumentNullException(nameof(items), "Invoice items are required");

            byte[] pdfBytes;
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf, PageSize.A4);

                try
                {
                    // Use the existing font instances instead of creating new ones
                    document.SetFont(_regularFont);
                    document.SetMargins(20, 20, 20, 20);

                    // Add content using class-level font variables
                    AddHeader(document, _boldFont, _regularFont);
                    AddInvoiceInfo(document, invoice, _boldFont, _regularFont);
                    AddItemsTable(document, items, _boldFont, _regularFont);
                    AddTotalsSection(document, items, invoice, _boldFont, _regularFont);
                    AddFooter(document, _regularFont);

                    document.Close();
                    writer.Close();
                    pdf.Close();

                    pdfBytes = memoryStream.ToArray();
                }
                catch (Exception ex)
                {
                    document?.Close();
                    writer?.Close();
                    pdf?.Close();
                    throw new Exception($"Error generating PDF: {ex.Message}", ex);
                }
            }

            return pdfBytes;
        }

        private void AddHeader(Document document, PdfFont boldFont, PdfFont regularFont)
        {
            // Create outer table with rounded border
            Table outerTable = new Table(1);
            outerTable.SetWidth(UnitValue.CreatePercentValue(100));

            // Create cell for outer border with rounded corners
            Cell outerCell = new Cell()
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderRadius(new BorderRadius(10))
                .SetPadding(15);

            // Inner table for content with two columns
            Table innerTable = new Table(2);
            innerTable.SetWidth(UnitValue.CreatePercentValue(100));

            // Left side content
            Cell leftCell = new Cell().SetBorder(Border.NO_BORDER);

            // SAFIN AHMED in red
            Paragraph companyNameEn = new Paragraph("SAFIN AHMED")
                .SetFont(boldFont)
                .SetFontSize(16)
                .SetFontColor(ColorConstants.RED);

            // Company in red
            Paragraph companyText = new Paragraph("Company")
                .SetFont(boldFont)
                .SetFontSize(14)
                .SetFontColor(ColorConstants.RED);

            // For General Trading in red
            Paragraph tradingText = new Paragraph("For General Trading")
                .SetFont(boldFont)
                .SetFontSize(14)
                .SetFontColor(ColorConstants.RED);

            // Contact details
            Paragraph website = new Paragraph("Website :    www.Safinahmed.krd")
                .SetFont(regularFont)
                .SetFontSize(10);

            Paragraph mobNo = new Paragraph("Mob.No. :    +964 770 640 0456    +964 770 244 2602")
                .SetFont(regularFont)
                .SetFontSize(10);

            leftCell.Add(companyNameEn);
            leftCell.Add(companyText);
            leftCell.Add(tradingText);
            leftCell.Add(new Paragraph().SetMarginBottom(5)); // Spacing
            leftCell.Add(website);
            leftCell.Add(mobNo);

            // Right side content
            Cell rightCell = new Cell().SetBorder(Border.NO_BORDER);

            // Arabic company name with RTL
            Paragraph companyNameAr = new Paragraph("شركة سفين احمد")
                .SetFont(regularFont)
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);

            // Arabic trading text with RTL
            Paragraph tradingTextAr = new Paragraph("للتجارة العامة")
                .SetFont(regularFont)
                .SetFontSize(14)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);

            // Arabic address with RTL
            Paragraph addressAr = new Paragraph("العنوان / زاخو - طريق ابراهيم الخليل- المجمع التجاري")
                .SetFont(regularFont)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);

            // Store numbers with label
            Paragraph storeLabel = new Paragraph("مخزن")
                .SetFont(regularFont)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);

            Paragraph storeNumbers = new Paragraph("+964 750 739 4409    +964 751 480 9968")
                .SetFont(regularFont)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT);

            rightCell.Add(companyNameAr);
            rightCell.Add(tradingTextAr);
            rightCell.Add(new Paragraph().SetMarginBottom(5)); // Spacing
            rightCell.Add(addressAr);
            rightCell.Add(storeLabel);
            rightCell.Add(storeNumbers);

            innerTable.AddCell(leftCell);
            innerTable.AddCell(rightCell);

            outerCell.Add(innerTable);
            outerTable.AddCell(outerCell);

            document.Add(outerTable);
        }

        private void AddInvoiceInfo(Document document, BuyFatoraViewModel invoice, PdfFont boldFont, PdfFont regularFont)
        {
            Table table = new Table(4);
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // First row with borders
            AddBorderedCell(table, "12:50 pm", TextAlignment.CENTER);
            AddBorderedCell(table, invoice.Date?.ToString("dd-MM-yyyy"), TextAlignment.CENTER);
            AddBorderedCell(table, invoice.Id.ToString(), TextAlignment.CENTER);
            AddBorderedCell(table, "رقم الفاتورة:", TextAlignment.RIGHT);

            // Second row with borders
            AddBorderedCell(table, "زاخو", TextAlignment.CENTER);
            AddBorderedCell(table, invoice.Customer ?? "", TextAlignment.CENTER);
            AddBorderedCell(table, "Customer", TextAlignment.CENTER);
            AddBorderedCell(table, ":ملاحظات", TextAlignment.RIGHT);

            document.Add(table);
        }

        private void AddItemsTable(Document document, List<InvoiceItemsViewModel> items, PdfFont boldFont, PdfFont regularFont)
        {
            Table table = new Table(new float[] { 2, 2, 2, 4, 2, 2 });
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Headers
            string[] headers = { "Total المجموع", "Price سعر", "Qty. الكمية",
                           "Item Details تفاصيل", "SKU كود", "Mould No القالب" };
            foreach (var header in headers)
            {
                Cell cell = new Cell()
                    .SetBackgroundColor(new DeviceRgb(200, 220, 255))
                    .SetPadding(5)
                    .SetTextAlignment(TextAlignment.CENTER);
                cell.Add(new Paragraph(header).SetFontSize(8));
                table.AddCell(cell);
            }

            // Items
            foreach (var item in items)
            {
                AddItemRow(table, item);
            }

            document.Add(table);
        }

        private void AddItemRow(Table table, InvoiceItemsViewModel item)
        {
            AddBorderedCell(table, $"$ {item.Total:N2}", TextAlignment.CENTER);
            AddBorderedCell(table, $"$ {item.Price:N2}", TextAlignment.CENTER);
            AddBorderedCell(table, item.Quantity.ToString(), TextAlignment.CENTER);
            AddBorderedCell(table, item.Name, TextAlignment.LEFT);
            AddBorderedCell(table, item.Sku, TextAlignment.CENTER);
            AddBorderedCell(table, item.Factory, TextAlignment.CENTER);
        }

        private void AddTotalsSection(Document document, List<InvoiceItemsViewModel> items, BuyFatoraViewModel invoice, PdfFont boldFont, PdfFont regularFont)
        {
            Table table = new Table(2);
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Left column
            Cell leftCell = new Cell().SetBorder(Border.NO_BORDER);
            leftCell.Add(new Paragraph($"Total cubes مجموع المكعبات: {items.Sum(i => i.Quantity * 0.5):N4}")
                .SetFontSize(8));
            leftCell.Add(new Paragraph($"Qty Total مجموع الكميات: {items.Sum(i => i.Quantity)}")
                .SetFontSize(8));
            leftCell.Add(new Paragraph($"Total weight مجموع الوزن: {items.Sum(i => i.Quantity * 8)}")
                .SetFontSize(8));

            // Right column
            Cell rightCell = new Cell().SetBorder(Border.NO_BORDER);
            rightCell.Add(new Paragraph($"Total المجموع: $ {invoice.Tootal:N2}")
                .SetFontSize(8).SetTextAlignment(TextAlignment.RIGHT));
            rightCell.Add(new Paragraph($"Porter cost الحمالية: $ 0.00")
                .SetFontSize(8).SetTextAlignment(TextAlignment.RIGHT));
            rightCell.Add(new Paragraph($"Discount الخصم: $ 0.00")
                .SetFontSize(8).SetTextAlignment(TextAlignment.RIGHT));
            rightCell.Add(new Paragraph($"Cash الواصل نقدا: $ {invoice.Payed:N2}")
                .SetFontSize(8).SetTextAlignment(TextAlignment.RIGHT));
            rightCell.Add(new Paragraph($"Remain Of Invoice المتبقي من الفاتورة: $ {invoice.Remaing:N2}")
                .SetFontSize(8).SetTextAlignment(TextAlignment.RIGHT));

            table.AddCell(leftCell);
            table.AddCell(rightCell);
            document.Add(table);
        }

        private void AddFooter(Document document, PdfFont regularFont)
        {
            document.Add(new Paragraph("Programmer : Shivan Mizury          www.Mizury-Software.com")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(8)
                .SetMarginTop(20));
        }

        private void AddBorderedCell(Table table, string text, TextAlignment alignment)
        {
            Cell cell = new Cell()
                .SetBorder(new SolidBorder(0.5f))
                .SetPadding(5);
            cell.Add(new Paragraph(text)
                .SetFontSize(8)
                .SetTextAlignment(alignment));
            table.AddCell(cell);
        }

        public void Dispose()
        {
            _boldFont = null;
            _regularFont = null;
            GC.SuppressFinalize(this);
        }
    }
}
