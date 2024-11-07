using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;

using sacmy.Shared.ViewModels.InvoiceViewModel;
using sacmy.Shared.ViewModels.TrackViewModel;
using System.Drawing;
using System.Threading.Tasks;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
       

        public InvoiceController(SafeenCompanyDbContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetInvoice()
        {
            try
            {
                var invoices = await _context.BuyFatoras
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Type)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.TaskNotes)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Status)
                    .OrderByDescending(e => e.Now)
                    .ToListAsync();

                var invoiceViewModels = new List<InvoiceViewModel>();


                foreach (var invoice in invoices)
                {

                    var lastTask = invoice.Tasks?.LastOrDefault();

                    if (lastTask != null)
                    {
                        var taskType = lastTask.Type?.TypeAr;
                        var taskStatus = lastTask.Status?.StateEn;
                        var lastTaskComment = lastTask.TaskNotes?.LastOrDefault()?.Note;

                        invoiceViewModels.Add(new InvoiceViewModel
                        {
                            Id = invoice.Id,
                            CustomerName = invoice.Customer,
                            CustomerType = invoice.CostType,
                            Address = invoice.Address,
                            InvoiceBranch = invoice.Subb,
                            Total = Convert.ToDouble(invoice.Tootal.ToString()),
                            IsCompleted = invoice.Checkeed??false,
                            DateTime = invoice.Now ?? DateTime.Today,
                            TaskId = lastTask.Id,
                            TaskStatus = taskStatus,
                            LastComment = lastTaskComment,
                        });
                    }

                    else
                    {
                        invoiceViewModels.Add(new InvoiceViewModel
                        {
                            Id = invoice.Id,
                            CustomerName = invoice.Customer,
                            CustomerType = invoice.CostType,
                            Address = invoice.Address,
                            InvoiceBranch = invoice.Subb,
                            Total = Convert.ToDouble(invoice.Tootal.ToString()),
                            IsCompleted = invoice.Checkeed ?? false,
                            DateTime = invoice.Now ?? DateTime.Today,
                            TaskId = null,
                            TaskStatus = null,
                            LastComment = null,
                        });
                    }
                }

                return Ok(invoiceViewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("InvoiceCounts")]
        public async Task<ActionResult<IEnumerable<InvoiceTypeViewModel>>> GetInvoiceCounts(DateTime? startDate = null, DateTime? endDate = null, string branch = null)
        {
            IQueryable<BuyFatora> query = _context.BuyFatoras;

            if (startDate.HasValue && endDate.HasValue)
            {
                if (branch != null)
                {
                    query = query.Where(i => i.Datee >= startDate.Value.Date && i.Datee <= endDate.Value.Date && i.Subb == branch);
                }
                else
                {
                    query = query.Where(i => i.Datee >= startDate.Value.Date && i.Datee <= endDate.Value.Date);
                }
            }

            // حساب إجمالي عدد الفواتير
            var totalInvoices = await query.CountAsync();

            var invoiceCounts = await query
                .GroupBy(i => i.CostType)
                .Select(g => new InvoiceTypeViewModel
                {
                    Type = g.Key,
                    InvoiceTypeCount = g.Count(),
                    TotalAmount = double.Parse(g.Sum(e => e.Tootal).ToString()),
                    Percentage = (double)g.Count() / totalInvoices * 100
                })
                .ToListAsync();

            return Ok(invoiceCounts);
        }

        [HttpGet("GetInvoiceItems")]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetInvoiceItems(int InvoiceId)
        {
            var invoiceItemsList = await _context.BuyFatoraItems.Where(e => e.Id == InvoiceId).Select(e => new InvoiceItemsViewModel
            {
                Id=e.Id,
                PatternCode = e.CodIqd,
                Sku = e.Codd,
                Name = e.Itemm,
                Factory = e.Factoryy,
                Price = e.Prise??0,
                Quantity = int.Parse(e.Quantity.ToString()),
                Total = e.Total??0,
                Cost = e.PurchasePrise??0,
                Batch = e.Wajba
            }).ToListAsync();

            return Ok(invoiceItemsList);
        }

        //[HttpPost("GenerateItemsPdf")]
        //public async Task<IActionResult> GenerateItemsPdf([FromBody] InvoiceItemsPdfRequest request)
        //{
        //    using (var doc = new PdfDocument())
        //    {
        //        var page = doc.AddPage();
        //        page.Size = PageSize.A4;
        //        page.Orientation = PageOrientation.Landscape;

        //        var gfx = XGraphics.FromPdfPage(page);

        //        // Use the static fonts
        //        var headerBlue = XColor.FromArgb(93, 158, 253);
        //        var borderGrey = XColor.FromArgb(200, 200, 200);


        //        const int startX = 50;  // Starting X position
        //        const int marginTop = 50;
        //        var pageWidth = page.Width.Point;

        //        // Company header (bilingual)
        //        gfx.DrawString("Company Name | اسم الشركة", TitleFont, XBrushes.Black, startX, marginTop);

        //        // Report title (bilingual)
        //        gfx.DrawString("Invoice Items Report | تقرير عناصر الفاتورة", ArabicBoldFont, XBrushes.Black,
        //            new XRect(startX, marginTop - 10, pageWidth - 2 * startX, 30),
        //            XStringFormats.Center);

        //        // Header info section
        //        var y = marginTop + 40;
        //        var infoColumnWidth = (pageWidth - 2 * startX) / 2;

        //        // Left column info (bilingual)
        //        gfx.DrawString("Invoice Information | معلومات الفاتورة:", ArabicBoldFont, XBrushes.Black, startX, y);
        //        y += 20;
        //        gfx.DrawString($"Invoice ID | رقم الفاتورة: {request.InvoiceId}", ArabicRegularFont, XBrushes.Black, startX, y);
        //        y += 15;
        //        gfx.DrawString($"Customer | العميل: {request.CustomerName}", ArabicRegularFont, XBrushes.Black, startX, y);

        //        // Right column info (bilingual)
        //        var rightColumn = startX + infoColumnWidth;
        //        y = marginTop + 40;
        //        gfx.DrawString("Date Information | معلومات التاريخ:", ArabicBoldFont, XBrushes.Black, rightColumn, y);
        //        y += 20;
        //        gfx.DrawString($"Invoice Date | تاريخ الفاتورة: {request.InvoiceDate:dd/MM/yyyy}", ArabicRegularFont, XBrushes.Black, rightColumn, y);
        //        y += 15;
        //        gfx.DrawString($"Report Date | تاريخ التقرير: {request.GenerationDate:dd/MM/yyyy HH:mm}", ArabicRegularFont, XBrushes.Black, rightColumn, y);

        //        // Table header
        //        y += 40;
        //        var startY = y;

        //        // Define columns with bilingual headers and widths
        //        var columns = new[]
        //        {
        //            ("Pattern Code | رمز النمط", 100),
        //            ("SKU | رقم المنتج", 100),
        //            ("Factory | المصنع", 120),
        //            ("Price | السعر", 80),
        //            ("Qty | الكمية", 60),
        //            ("Total | المجموع", 80),
        //            ("Check | تحقق", 60),
        //            ("Notes | ملاحظات", 150)
        //        };

        //        // Draw table header
        //        var headerHeight = 25;
        //        var tableWidth = columns.Sum(c => c.Item2);
        //        gfx.DrawRectangle(new XSolidBrush(headerBlue), startX, y, tableWidth, headerHeight);

        //        // Draw header text
        //        var currentX = startX;
        //        foreach (var (title, width) in columns)
        //        {
        //            gfx.DrawString(title, ArabicBoldFont , XBrushes.White, currentX, y + 17);
        //            currentX += width;
        //        }

        //        // Reset y position for data rows
        //        y += headerHeight;

        //        // Draw rows
        //        var rowHeight = 20;
        //        bool isAlternate = false;

        //        foreach (var item in request.Items)
        //        {
        //            // Check for new page
        //            if (y > page.Height.Point - 50)
        //            {
        //                page = doc.AddPage();
        //                page.Orientation = PageOrientation.Landscape;
        //                gfx = XGraphics.FromPdfPage(page);
        //                y = marginTop;
        //            }

        //            // Alternate row background
        //            if (isAlternate)
        //            {
        //                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(244, 249, 255)),
        //                    startX, y, tableWidth, rowHeight);
        //            }

        //            var xPos = startX;

        //            // Draw row data
        //            DrawCell(gfx, item.PatternCode, ArabicRegularFont, xPos, y + 15); xPos += columns[0].Item2;
        //            DrawCell(gfx, item.Sku, ArabicRegularFont, xPos, y + 15); xPos += columns[1].Item2;
        //            DrawCell(gfx, item.Factory, ArabicRegularFont, xPos, y + 15); xPos += columns[2].Item2;
        //            DrawCell(gfx, item.Price.ToString("C2"), ArabicRegularFont, xPos, y + 15); xPos += columns[3].Item2;
        //            DrawCell(gfx, item.Quantity.ToString(), ArabicRegularFont, xPos, y + 15); xPos += columns[4].Item2;
        //            DrawCell(gfx, item.Total.ToString("C2"), ArabicRegularFont, xPos, y + 15); xPos += columns[5].Item2;

        //            // Checkbox
        //            var checkboxSize = 10;
        //            var checkboxY = y + 5;
        //            gfx.DrawRectangle(XPens.Black, xPos + 25, checkboxY, checkboxSize, checkboxSize);
        //            if (item.IsChecked)
        //            {
        //                gfx.DrawString("✓", ArabicBoldFont, XBrushes.Black, xPos + 25, y + 15);
        //            }
        //            xPos += columns[6].Item2;

        //            // Notes
        //            DrawCell(gfx, item.Notes, ArabicRegularFont, xPos, y + 15);

        //            y += rowHeight;
        //            isAlternate = !isAlternate;
        //        }

        //        // Table border
        //        gfx.DrawRectangle(XPens.Gray, startX, startY, tableWidth, y - startY);

        //        // Column dividers
        //        var verticalX = startX;
        //        foreach (var (_, width) in columns)
        //        {
        //            verticalX += width;
        //            gfx.DrawLine(XPens.Gray, verticalX, startY, verticalX, y);
        //        }

        //        // Summary section (bilingual)
        //        y += 20;
        //        var totalItems = request.Items.Count;
        //        var totalQuantity = request.Items.Sum(i => i.Quantity);
        //        var totalAmount = request.Items.Sum(i => i.Total);

        //        gfx.DrawString("Summary | الملخص:", ArabicBoldFont, XBrushes.Black, startX, y);
        //        y += 20;
        //        gfx.DrawString($"Total Items | إجمالي العناصر: {totalItems}", ArabicRegularFont, XBrushes.Black, startX, y);
        //        gfx.DrawString($"Total Quantity | الكمية الإجمالية: {totalQuantity}", ArabicRegularFont, XBrushes.Black, startX + 200, y);
        //        gfx.DrawString($"Total Amount | المبلغ الإجمالي: {totalAmount:C2}", ArabicRegularFont, XBrushes.Black, startX + 400, y);

        //        using (var ms = new MemoryStream())
        //        {
        //            doc.Save(ms);
        //            return File(ms.ToArray(), "application/pdf", $"invoice_{request.InvoiceId}_items.pdf");
        //        }
        //    }
        //}

        //private void DrawCell(XGraphics gfx, string text, XFont font, double x, double y, double maxWidth = 0)
        //{
        //    if (string.IsNullOrEmpty(text)) return;

        //    if (maxWidth > 0 && text.Length > 20)
        //    {
        //        text = text.Substring(0, 17) + "...";
        //    }

        //    gfx.DrawString(text, font, XBrushes.Black, x, y);
        //}

        [HttpGet("GetInvoiceTrack")]
        public async Task<IActionResult> GetCustomerTrack([FromQuery] int invoiceId)
        {
            try
            {
                var list = await _context.Tracks.
                                 Include(e => e.Employe)
                                .Include(e => e.Type)
                                .Include(e => e.Customer)
                                .Include(e => e.TrackComments)
                                .ThenInclude(e => e.TrackCommentStates)
                                .Where(e => e.InvoiceId == invoiceId)
                                .Select(e => new GetInvoiceTrackViewModel
                                {
                                    InvoiceId = e.InvoiceId ?? 0,
                                    TrackId = e.Id,
                                    Type = e.Type.TypeAr,
                                    EmployeeId = e.Employe.Id,
                                    EmployeeName = e.Employe.FirstName + " " + e.Employe.LastName,
                                    CreatedDate = e.CreatedDate
                                }).ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TransferInvoice")]
        public async Task<IActionResult> ProcessBuyFatora([FromBody] TransferInvoiceViewModel transferInvoiceViewModel)
        {
            var buyFatora = await _context.BuyFatoras.FirstOrDefaultAsync(b => b.Id == transferInvoiceViewModel.SaleBillId);
            List<BuyFatoraItem> buyFatoraItems = await _context.BuyFatoraItems.Where(item => item.Id == transferInvoiceViewModel.SaleBillId).ToListAsync();
            EventCostumer delete_event_costomer = await _context.EventCostumers.FirstOrDefaultAsync(e => e.EventId.Trim().ToLower() == ("bu - "+buyFatora.Id).Trim());
            List<BuyFatoraItem> newBuyFatoraItems = new List<BuyFatoraItem>();
            List<PurchaseInvoiceItem> PurchaseFatoraItems = new List<PurchaseInvoiceItem>();
            List<BuyFatoraItem> lastBuyFatoraItems = new List<BuyFatoraItem>();
            List<PurchaseInvoiceItem> lastPurchaseFatoraItems = new List<PurchaseInvoiceItem>();

            EventCostumer event_costumer = new EventCostumer();
            //EventCostumer event_costumer_for_company = new EventCostumer();
            EventCompany event_company = new EventCompany();

            

            int lastId = await _context.BuyFatoras
                               .OrderByDescending(f => f.Id)
                               .Select(f => f.Id)
                               .FirstOrDefaultAsync();

            int lastPurchhaseBillId = await _context.PurchaseInvoices
                                            .OrderByDescending(f => f.Id)
                                            .Select(f => f.Id)
                                            .FirstOrDefaultAsync();

            int event_company_Id = await _context.EventCompanies
                                            .OrderByDescending(f => f.IdEvent)
                                            .Select(f => f.IdEvent)
                                            .FirstOrDefaultAsync();

            #region Stage 1

            foreach (BuyFatoraItem item in buyFatoraItems)
            {
                BuyFatoraItem buyFatoraItem = new BuyFatoraItem
                {
                    Id = lastId + 1,
                    Secode = item.Secode,
                    Codd = item.Codd,
                    Typee = item.Typee,
                    Factoryy = item.Factoryy,
                    Weznn = item.Weznn,
                    QiyasUnit = item.QiyasUnit,
                    Countt = item.Countt,
                    Prise = item.PurchasePrise,
                    Quantity = item.Quantity,
                    PurchasePrise = item.PurchasePrise,
                    Total = item.PurchasePrise * decimal.Parse(item.Quantity.ToString()),
                    BoxContain = item.BoxContain,
                    CodIqd = item.CodIqd,
                    BuId = 0,
                    QttRemaining = item.QttRemaining,
                    Itemm = item.Itemm,
                    Rub7Karton = item.Rub7Karton,
                    Subb = item.Subb,
                    TtalPoints = item.TtalPoints,
                    Wajba = item.Wajba,
                    Points = item.Points,
                    IsDeleted = item.IsDeleted,
                };

                newBuyFatoraItems.Add(buyFatoraItem);
            }

            var newBuyFatora = new BuyFatora
            {
                Id = lastId + 1,
                Idd = buyFatora.Idd,
                Date = buyFatora.Date,
                Customer = transferInvoiceViewModel.customer,
                Address = transferInvoiceViewModel.Branch,
                Mob = buyFatora.Mob,
                Notes = buyFatora.Notes,
                Dolar = buyFatora.Dolar,
                Now = buyFatora.Now,
                Treasurer = buyFatora.Treasurer,
                Uuser = buyFatora.Uuser,
                Payed = buyFatora.Payed,
                Tootal = (decimal)(buyFatoraItems.Sum(item => (double.Parse(item.PurchasePrise.ToString())) * item.Quantity)),
                Remaing = (decimal)(buyFatoraItems.Sum(item => (double.Parse(item.PurchasePrise.ToString())) * item.Quantity)),
                Mandob = buyFatora.Mandob,
                ManCcount = buyFatora.ManCcount,
                Ijraaa = buyFatora.Ijraaa,
                Ijraaa2 = buyFatora.Ijraaa2,
                Driver = buyFatora.Driver,
                CarNo = buyFatora.CarNo,
                DriverMob = buyFatora.DriverMob,
                Subb = buyFatora.Subb,
                EventId = buyFatora.EventId,
                Hamalya = buyFatora.Hamalya,
                Datee = buyFatora.Datee,
                Checkeed = buyFatora.Checkeed,
                Nsba = buyFatora.Nsba,
                Nsbaother = buyFatora.Nsbaother,
                CostType = buyFatora.CostType,
                Notify = buyFatora.Notify,
                Discount = buyFatora.Discount,
                TotalPoints = buyFatora.TotalPoints,
                IsDeleted = buyFatora.IsDeleted
            };

            
/*           event_costumer_for_company.IdEvent = 0;
            event_costumer_for_company.Costumer = transferInvoiceViewModel.customer;
            event_costumer_for_company.EventId = "Sell_Ex - " + (lastId + 1);
            event_costumer_for_company.Trans = 1;
            event_costumer_for_company.Ttttotal = newBuyFatora.Remaing;
            event_costumer_for_company.Datee = DateTime.Now;
            event_costumer_for_company.Notess = null;
            event_costumer_for_company.MtabqaDate = null;
            event_costumer_for_company.Typeevent = "فاتورة ألبيع";
            event_costumer_for_company.Noww = DateTime.Now;
            event_costumer_for_company.Checkeed = false;
            event_costumer_for_company.Subb = newBuyFatora.Subb;*/

            try
            {
                await _context.BuyFatoras.AddAsync(newBuyFatora);
                await _context.BuyFatoraItems.AddRangeAsync(newBuyFatoraItems);
                //await _context.EventCostumers.AddRangeAsync(event_costumer_for_company);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            #endregion

            #region Stage 2 : Create Purchase Invoice

            PurchaseInvoice purchaseInvoice = new PurchaseInvoice
            {
                Id = lastPurchhaseBillId + 1,
                Idd = 0,
                Address = transferInvoiceViewModel.Address,
                CarNo = transferInvoiceViewModel.CarNo,
                Company = transferInvoiceViewModel.Company,
                Date = DateTime.Now,
                Dolar = 0,
                Driver = null,
                DriverMob = null,
                Now = DateTime.Now,
                EventId = "Pur-ex - " + (lastPurchhaseBillId + 1),
                Subb = transferInvoiceViewModel.Branch,
                Wajba = buyFatora.Subb + " -> " + transferInvoiceViewModel.Branch + DateTime.Now.ToString("yyyy-MM-dd-hh-mm"),
                Notes = "test transfer invoice",
                Uuser = "Mustafa",
                Treasurer = "Treasuer",
            };

            foreach (BuyFatoraItem item in newBuyFatoraItems)
            {
                PurchaseInvoiceItem purchaseInvoiceItem = new PurchaseInvoiceItem
                {
                    Id = purchaseInvoice.Id,
                    Codd = item.Codd,
                    CodIqd = item.CodIqd,
                    Secode = item.Secode,
                    BoxContain = item.BoxContain,
                    Countt = item.Countt,
                    Factoryy = item.Factoryy,
                    FirstQtty = item.Quantity,
                    Quantity = item.Quantity,
                    Itemm = item.Itemm,
                    Masraf = 0,
                    Naqis = 0,
                    Prise = item.Prise,
                    PuId = 0,
                    QiyasUnit = item.QiyasUnit,
                    Ziyada = 0,
                    Subb = transferInvoiceViewModel.Branch,
                    Total = item.Prise * decimal.Parse(item.Quantity.ToString()),
                    TotlPrise = item.Prise , //Price  + Masaraf 
                    Typee = "زجاجيات",
                    Wajba = purchaseInvoice.Wajba,
                    Weznn = item.Weznn,
                };
                PurchaseFatoraItems.Add(purchaseInvoiceItem);
            }

            try
            {
                await _context.PurchaseInvoices.AddAsync(purchaseInvoice);
                await _context.PurchaseInvoiceItems.AddRangeAsync(PurchaseFatoraItems);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                event_company.Company = purchaseInvoice.Company;
                event_company.EventId = "Pur-ex - " + purchaseInvoice.Id;
                event_company.Trans = 1;
                event_company.Ttttotal = PurchaseFatoraItems.Sum(e => e.Total);
                event_company.Datee = DateTime.Now;
                event_company.Notess = "Generated By Website";
                event_company.MtabqaDate = null;
                event_company.Typeevent = "فاتورة الشراء-تحويل";
                event_company.Noww = DateTime.Now;
                event_company.MtabqaDatee = null;
                event_company.Subb = purchaseInvoice.Subb;
                await _context.EventCompanies.AddAsync(event_company);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            #endregion

            #region Remove Original BuyFatora and BuyFatoraItems

            try
            {
                _context.BuyFatoras.Remove(buyFatora);
                _context.BuyFatoraItems.RemoveRange(buyFatoraItems);
                _context.EventCostumers.RemoveRange(delete_event_costomer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            #endregion

            #region Stage 3 : re-Create The Buy Fatora

            foreach (BuyFatoraItem item in buyFatoraItems)
            {
                BuyFatoraItem buyFatoraItem = new BuyFatoraItem
                {
                    Id = buyFatora.Id,
                    Secode = item.Secode,
                    Codd = item.Codd,
                    Typee = item.Typee,
                    Factoryy = item.Factoryy,
                    Weznn = item.Weznn,
                    QiyasUnit = item.QiyasUnit,
                    Countt = item.Countt,
                    Prise = item.Prise,
                    Quantity = item.Quantity,
                    PurchasePrise = item.PurchasePrise,
                    Total = item.Total,
                    BoxContain = item.BoxContain,
                    CodIqd = item.CodIqd,
                    BuId = 0,
                    QttRemaining = item.QttRemaining,
                    Itemm = item.Itemm,
                    Rub7Karton = item.Rub7Karton,
                    Subb = transferInvoiceViewModel.Branch,
                    TtalPoints = item.TtalPoints,
                    Wajba = purchaseInvoice.Wajba,
                    Points = item.Points,
                    IsDeleted = item.IsDeleted,
                };

                lastBuyFatoraItems.Add(buyFatoraItem);
            }

            var lastBuyFatora = new BuyFatora
            {
                Id = buyFatora.Id,
                Idd = buyFatora.Idd,
                Date = DateTime.Now,
                Customer = buyFatora.Customer,
                Address = transferInvoiceViewModel.Address,
                Mob = buyFatora.Mob,
                Notes = buyFatora.Notes,
                Dolar = buyFatora.Dolar,
                Now = DateTime.Now,
                Treasurer = buyFatora.Treasurer,
                Uuser = buyFatora.Uuser,
                Payed = buyFatora.Payed,
                Tootal = buyFatora.Tootal,
                Mandob = buyFatora.Mandob,
                ManCcount = buyFatora.ManCcount,
                Ijraaa = buyFatora.Ijraaa,
                Ijraaa2 = buyFatora.Ijraaa2,
                Driver = buyFatora.Driver,
                CarNo = buyFatora.CarNo,
                DriverMob = buyFatora.DriverMob,
                Subb = transferInvoiceViewModel.Branch,
                EventId = "BU - " + (lastId + 2),
                Hamalya = buyFatora.Hamalya,
                Datee = DateTime.Now,
                Checkeed = buyFatora.Checkeed,
                Nsba = buyFatora.Nsba,
                Nsbaother = buyFatora.Nsbaother,
                CostType = buyFatora.CostType,
                Notify = buyFatora.Notify,
                Discount = buyFatora.Discount,
                TotalPoints = buyFatora.TotalPoints,
                IsDeleted = buyFatora.IsDeleted
            };

            try
            {
                await _context.BuyFatoras.AddAsync(lastBuyFatora);
                await _context.BuyFatoraItems.AddRangeAsync(lastBuyFatoraItems);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            event_costumer.IdEvent = 0;
            event_costumer.Costumer = lastBuyFatora.Customer;
            event_costumer.EventId = "BU - " + (buyFatora.Id);
            event_costumer.Trans = 1;
            event_costumer.Ttttotal = lastBuyFatora.Tootal;
            event_costumer.Datee = DateTime.Now;
            event_costumer.Notess = null;
            event_costumer.MtabqaDate = null;
            event_costumer.Typeevent = "فاتورة ألبيع";
            event_costumer.Noww = DateTime.Now;
            event_costumer.Checkeed = false;
            event_costumer.Subb = lastBuyFatora.Subb;

            try
            {
                await _context.EventCostumers.AddAsync(event_costumer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            #endregion

            

            return Ok("BuyFatora processed successfully.");
        }

        [HttpGet("GetUncompleteInvoices")]
        public async Task<IActionResult> GetUncompleteInvoices()
        {
            try
            {
                var invoices = await _context.BuyFatoras
                    .Where(e => e.Checkeed == true)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Type)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.TaskNotes)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Status)
                    .OrderByDescending(e => e.Now)
                    .ToListAsync();

                var invoiceViewModels = new List<InvoiceViewModel>();


                foreach (var invoice in invoices)
                {

                    var lastTask = invoice.Tasks?.LastOrDefault();

                    if (lastTask != null)
                    {
                        var taskType = lastTask.Type?.TypeAr;
                        var taskStatus = lastTask.Status?.StateEn;
                        var lastTaskComment = lastTask.TaskNotes?.LastOrDefault()?.Note;

                        invoiceViewModels.Add(new InvoiceViewModel
                        {
                            Id = invoice.Id,
                            CustomerName = invoice.Customer,
                            CustomerType = invoice.CostType,
                            Address = invoice.Address,
                            InvoiceBranch = invoice.Subb,
                            Total = Convert.ToDouble(invoice.Tootal.ToString()),
                            DateTime = invoice.Now ?? DateTime.Today,
                            TaskId = lastTask.Id,
                            TaskStatus = taskStatus, 
                            LastComment = lastTaskComment,
                        });
                    }

                    else {
                        invoiceViewModels.Add(new InvoiceViewModel
                        {
                            Id = invoice.Id,
                            CustomerName = invoice.Customer,
                            CustomerType = invoice.CostType,
                            Address = invoice.Address,
                            InvoiceBranch = invoice.Subb,
                            Total = Convert.ToDouble(invoice.Tootal.ToString()),
                            DateTime = invoice.Now ?? DateTime.Today,
                            TaskId = null,
                            TaskStatus = null,
                            LastComment = null,
                        });
                    }
                }

                return Ok(invoiceViewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /*        [HttpPost("RemoveInvoice")]
                public async Task<IActionResult> RemoveInvoice([FromBody]int InvoiceId)
                {
                    try
                    {
                        BuyFatora buyFatora = await _context.BuyFatoras.FirstOrDefaultAsync(b => b.Id == InvoiceId);
                        List<BuyFatoraItem> buyFatoraItems = await _context.BuyFatoraItems.Where(item => item.Id == InvoiceId).ToListAsync();

                        _context.BuyFatoras.Remove(buyFatora);
                        _context.BuyFatoraItems.RemoveRange(buyFatoraItems);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.ToString());
                    }

                }*/

    }

}
