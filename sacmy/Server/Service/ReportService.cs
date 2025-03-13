using FastReport.Export.PdfSimple;
using FastReport;

using sacmy.Shared.ViewModels.InvoiceViewModel;
using FastReport.Table;

namespace sacmy.Server.Service
{
    public class ReportService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ReportService> _logger;

        public ReportService(
            IWebHostEnvironment webHostEnvironment,
            ILogger<ReportService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public byte[] GenerateInvoiceReport(BuyFatoraViewModel invoice, List<InvoiceItemsViewModel> invoiceItems)
        {
            try
            {
                FastReport.Utils.Config.WebMode = true;
                var report = new FastReport.Report();
                string rootpath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "invoice", "SafinAhmed.frx");
                if (!System.IO.File.Exists(rootpath))
                {
                    _logger.LogError($"Report template not found at path: {rootpath}");
                    throw new FileNotFoundException($"Report template not found at path: {rootpath}");
                }
                report.Load(rootpath);

                // Set report parameters
                (report.FindObject("ID") as TextObject).Text = invoice.Id.ToString();
                (report.FindObject("CustomerName") as TextObject).Text = invoice.Customer;
                (report.FindObject("Date") as TextObject).Text = invoice.Date?.ToString("MM-dd-yyyy");
                (report.FindObject("Time") as TextObject).Text = invoice.Date?.ToString("hh:mm tt");
                (report.FindObject("Address") as TextObject).Text = invoice.CustomerAddress ?? "";
                (report.FindObject("total") as TextObject).Text = $"$ {invoice.Tootal:N2}";
                (report.FindObject("porterCost") as TextObject).Text = $"$ {0:N2}";
                (report.FindObject("discount") as TextObject).Text = "$ 0.0";
                (report.FindObject("Cash") as TextObject).Text = $"$ {invoice.Payed:N2}";
                (report.FindObject("totalCube") as TextObject).Text = invoiceItems.Count.ToString();
                (report.FindObject("qtyTotal") as TextObject).Text = invoiceItems.Sum(i => i.Quantity).ToString();
                (report.FindObject("totalWeight") as TextObject).Text = "0";

                // Create data table
                var dataTable = new System.Data.DataTable("Invoice");
                dataTable.Columns.Add("total", typeof(string));
                dataTable.Columns.Add("price", typeof(string));
                dataTable.Columns.Add("qty", typeof(string));
                dataTable.Columns.Add("qty2", typeof(string));
                dataTable.Columns.Add("details", typeof(string));
                dataTable.Columns.Add("sku", typeof(string));
                dataTable.Columns.Add("no", typeof(string));

                // Add data rows with trimmed name
                foreach (var item in invoiceItems)
                {
                    // Trim the Name to a maximum of 25 characters
                    string trimmedName = item.Name;
                    if (!string.IsNullOrEmpty(trimmedName) && trimmedName.Length > 25)
                    {
                        trimmedName = trimmedName.Substring(0, 25) + "...";
                    }

                    dataTable.Rows.Add(
                        item.Total.ToString("N2"),
                        item.Price.ToString("N2"),
                        item.Quantity.ToString(),
                        item.BoxContain,
                        trimmedName,   // Use the trimmed name here
                        item.Sku,
                        item.PatternCode
                    );
                }

                // Register the data
                report.RegisterData(dataTable, "Invoice");

                // Find the data band and set its data source
                var dataBand = report.FindObject("DataInvoice") as DataBand;
                if (dataBand != null)
                {
                    var ds = report.GetDataSource("Invoice");
                    ds.Enabled = true;  // This is the crucial line!
                    dataBand.DataSource = ds;
                }

                // Find the table and set its row count
                var table = report.FindObject("Table2") as TableObject;
                if (table != null)
                {
                    // Set the number of rows in the table
                    // This will add more rows if needed
                    table.RowCount = invoiceItems.Count + 1; // +1 for header row
                }

                report.Prepare();

                // Export to PDF
                using var stream = new MemoryStream();
                var pdfExport = new PDFSimpleExport();
                pdfExport.Title = "New Invoice Report";
                report.Export(pdfExport, stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating invoice report for invoice {invoice?.Id}");
                throw;
            }
        }

        private static DataBand RegisterDynamicFastReportData(FastReport.Report report, string connectionAlias, IEnumerable<object> dataSet, string filterExpression = null, string dataBand = null)
        {
            report.RegisterData(dataSet, connectionAlias, 3);
            var ds = report.GetDataSource(connectionAlias);
            ds.Enabled = true;
            ds.Init();
            ds.EnsureInit();

            if (dataBand != null)
            {
                var band = report.FindObject(dataBand) as FastReport.DataBand;
                if (band != null)
                {
                    band.DataSource = ds;
                    band.InitDataSource();
                    return band;
                }
            }

            return null;
        }
    }
}
