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
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(SafeenCompanyDbContext context , ILogger<InvoiceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetInvoice([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 20,[FromQuery] bool? isCompleted = null,[FromQuery] string searchTerm = null)
        {
            try
            {
                _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));

                var query = _context.BuyFatoras.AsQueryable();

                if (isCompleted.HasValue)
                {
                    query = query.Where(i => i.Checkeed == isCompleted.Value);
                }


                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    query = query.Where(i =>
                        (i.Customer != null && i.Customer.ToLower().Contains(searchTerm)) ||
                        (i.CostType != null && i.CostType.ToLower().Contains(searchTerm)) ||
                        (i.Address != null && i.Address.ToLower().Contains(searchTerm)) ||
                        (i.Subb != null && i.Subb.ToLower().Contains(searchTerm))
                    );
                }

                // Get total count with all filters applied
                var totalCount = await query.CountAsync();

                // Calculate the number of records to skip
                int skip = (pageNumber - 1) * pageSize;

                // Execute the query with includes and pagination
                var invoices = await query
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Type)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.TaskNotes)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Status)
                    .OrderByDescending(e => e.Now)
                    .Skip(skip)
                    .Take(pageSize)
                    .AsSplitQuery()  
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

                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            bool includeInvoice = true;

                            
                            if ((taskStatus == null || !taskStatus.ToLower().Contains(searchTerm)) &&
                                (lastTaskComment == null || !lastTaskComment.ToLower().Contains(searchTerm)))
                            {
 
                                if (!(
                                    (invoice.Customer != null && invoice.Customer.ToLower().Contains(searchTerm)) ||
                                    (invoice.CostType != null && invoice.CostType.ToLower().Contains(searchTerm)) ||
                                    (invoice.Address != null && invoice.Address.ToLower().Contains(searchTerm)) ||
                                    (invoice.Subb != null && invoice.Subb.ToLower().Contains(searchTerm))
                                ))
                                {
                                    includeInvoice = false;
                                }
                            }

                            if (!includeInvoice)
                            {
                                continue; 
                            }
                        }

                        invoiceViewModels.Add(new InvoiceViewModel
                        {
                            Id = invoice.Id,
                            CustomerName = invoice.Customer,
                            CustomerType = invoice.CostType,
                            Address = invoice.Address,
                            InvoiceBranch = invoice.Subb,
                            Total = Convert.ToDouble(invoice.Tootal.ToString()),
                            IsCompleted = invoice.Checkeed ?? false,
                            IsPdfGenerated = invoice.IsPdfGenerated ?? false,
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
                            IsPdfGenerated = invoice.IsPdfGenerated ?? false,
                            DateTime = invoice.Now ?? DateTime.Today,
                            TaskId = null,
                            TaskStatus = null,
                            LastComment = null,
                        });
                    }
                }

                var paginationMetadata = new
                {
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };


                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

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
        public async Task<ActionResult<IEnumerable<InvoiceItemsViewModel>>> GetInvoiceItems(int InvoiceId)
        {
            try
            {
                // Step 1: Validate input
                _logger.LogInformation($"Starting GetInvoiceItems for InvoiceId: {InvoiceId}");

                if (InvoiceId <= 0)
                {
                    _logger.LogWarning($"Invalid Invoice ID provided: {InvoiceId}");
                    return BadRequest("Invalid Invoice ID.");
                }

                // Step 2: Check if invoice exists
                var invoiceExists = await _context.BuyFatoraItems
                    .AnyAsync(i => i.Id == InvoiceId);

                if (!invoiceExists)
                {
                    _logger.LogWarning($"No items found for Invoice ID: {InvoiceId}");
                    return NotFound($"No items found for Invoice ID: {InvoiceId}");
                }

                // Step 3: Get base invoice items
                var baseItems = await _context.BuyFatoraItems
                    .Where(i => i.Id == InvoiceId)
                    .ToListAsync();

                _logger.LogInformation($"Found {baseItems.Count} base items for Invoice ID: {InvoiceId}");

                // Debug information for base items
                var debugBaseItems = baseItems.Select(i => new
                {
                    i.Id,
                    i.Codd,
                    i.Itemm,
                    i.Quantity,
                    i.Prise
                }).ToList();

                _logger.LogDebug($"Base items debug info: {System.Text.Json.JsonSerializer.Serialize(debugBaseItems)}");

                // Step 4: Get related area information
                var itemSkus = baseItems.Select(i => i.Codd).Distinct().ToList();
                
                // Step 4: Get related area information
                var areaInfos = new Dictionary<string, Item>();

                if (itemSkus.Any())
                {
                    // Load area info one by one instead of using Contains
                    foreach (var sku in itemSkus)
                    {
                        var areaInfo = await _context.Items
                            .FirstOrDefaultAsync(a => a.Cod == sku);

                        if (areaInfo != null && !areaInfos.ContainsKey(sku))
                        {
                            areaInfos.Add(sku, areaInfo);
                        }
                    }
                }

                _logger.LogInformation($"Found {areaInfos.Count} area infos for {itemSkus.Count} distinct SKUs");

                _logger.LogInformation($"Found {areaInfos.Count} area infos for {itemSkus.Count} distinct SKUs");

                // Debug information for area infos
                var debugAreaInfos = areaInfos.Select(a => new
                {
                    Sku = a.Key,
                    Area = a.Value.Mkaab,
                    Weight = a.Value.Mkaab
                }).ToList();

                _logger.LogDebug($"Area infos debug info: {System.Text.Json.JsonSerializer.Serialize(debugAreaInfos)}");

                // Step 5: Build view models
                var result = new List<InvoiceItemsViewModel>();

                foreach (var item in baseItems)
                {
                    areaInfos.TryGetValue(item.Codd, out var areaInfo);

                    var viewModel = new InvoiceItemsViewModel
                    {
                        Id = item.Id,
                        PatternCode = item.CodIqd,
                        Sku = item.Codd,
                        Name = item.Itemm,
                        Factory = item.Factoryy,
                        Price = item.Prise ?? 0,
                        Quantity = (int)(item.Quantity ?? 0),
                        BoxContain = item.BoxContain,
                        Total = item.Total ?? 0,
                        Cost = item.PurchasePrise ?? 0,
                        Batch = item.Wajba,
                        Area = areaInfo != null ? (double?)areaInfo.Mkaab ?? 0 : 0,
                        Weight = areaInfo?.Mkaab ?? 0
                    };

                    result.Add(viewModel);
                }

                _logger.LogInformation($"Successfully built {result.Count} view models");

                // Step 6: Final validation
                if (!result.Any())
                {
                    _logger.LogWarning("No view models were created despite finding base items");
                    return NotFound("No items could be processed for the given Invoice ID");
                }

                // Step 7: Return success response
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing GetInvoiceItems for InvoiceId: {InvoiceId}");
                return StatusCode(500, "An error occurred while processing your request. Please check the logs for more details.");
            }
        }

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
        public async Task<IActionResult> GetUncompleteInvoices([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 15,[FromQuery] string searchTerm = null)
        {
            try
            {
                var query = _context.BuyFatoras
                    .Where(e => e.Checkeed == true)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Type)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.TaskNotes)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Status)
                    .OrderByDescending(e => e.Now)
                    .AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    query = query.Where(i =>
                        (i.Customer != null && i.Customer.ToLower().Contains(searchTerm)) ||
                        (i.CostType != null && i.CostType.ToLower().Contains(searchTerm)) ||
                        (i.Address != null && i.Address.ToLower().Contains(searchTerm)) ||
                        (i.Subb != null && i.Subb.ToLower().Contains(searchTerm))
                    );
                }

                // Get total count with filter applied
                var totalCount = await query.CountAsync();

                // Apply pagination
                var invoices = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
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

                        // If search term is provided, apply additional filtering for task-related fields
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            bool includeInvoice = true;

                            if ((taskStatus == null || !taskStatus.ToLower().Contains(searchTerm)) &&
                                (lastTaskComment == null || !lastTaskComment.ToLower().Contains(searchTerm)))
                            {
                                if (!(
                                    (invoice.Customer != null && invoice.Customer.ToLower().Contains(searchTerm)) ||
                                    (invoice.CostType != null && invoice.CostType.ToLower().Contains(searchTerm)) ||
                                    (invoice.Address != null && invoice.Address.ToLower().Contains(searchTerm)) ||
                                    (invoice.Subb != null && invoice.Subb.ToLower().Contains(searchTerm))
                                ))
                                {
                                    includeInvoice = false;
                                }
                            }

                            if (!includeInvoice)
                            {
                                continue;
                            }
                        }

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
                            DateTime = invoice.Now ?? DateTime.Today,
                            TaskId = null,
                            TaskStatus = null,
                            LastComment = null,
                        });
                    }
                }

                // Create pagination metadata
                var paginationMetadata = new
                {
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };

                // Add pagination metadata to response headers
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

                return Ok(invoiceViewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetMultipleInvoicesItems")]
        public async Task<ActionResult<IEnumerable<InvoiceItemsViewModel>>> GetMultipleInvoicesItems([FromBody] List<int> invoiceIds)
        {
            try
            {
                // Step 1: Validate input
                if (invoiceIds == null || !invoiceIds.Any())
                {
                    return BadRequest("Please provide at least one Invoice ID.");
                }
                if (invoiceIds.Any(id => id <= 0))
                {
                    return BadRequest("Invalid Invoice ID detected. All IDs must be greater than 0.");
                }

                // Step 2: Get base items using individual queries
                var baseItems = new List<BuyFatoraItem>();
                foreach (var invoiceId in invoiceIds)
                {
                    var items = await _context.BuyFatoraItems
                        .Where(i => i.Id == invoiceId)
                        .AsNoTracking()
                        .ToListAsync();
                    baseItems.AddRange(items);
                }

                if (!baseItems.Any())
                {
                    return NotFound($"No items found for the provided Invoice IDs: {string.Join(", ", invoiceIds)}");
                }

                // Step 3: Get unique SKUs
                var itemSkus = baseItems.Select(i => i.Codd).Distinct().ToList();

                // Step 4: Get area information
                var areaInfos = new Dictionary<string, Item>();
                foreach (var sku in itemSkus)
                {
                    var areaInfo = await _context.Items
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Cod == sku);

                    if (areaInfo != null && !areaInfos.ContainsKey(sku))
                    {
                        areaInfos.Add(sku, areaInfo);
                    }
                }

                // Step 5: Build the result
                var result = baseItems.Select(item =>
                {
                    areaInfos.TryGetValue(item.Codd, out var areaInfo);

                    return new InvoiceItemsViewModel
                    {
                        Id = item.Id,
                        PatternCode = item.CodIqd,
                        Sku = item.Codd,
                        Name = item.Itemm,
                        Factory = item.Factoryy,
                        Price = item.Prise ?? 0,
                        Quantity = (int)(item.Quantity ?? 0),
                        BoxContain = item.BoxContain,
                        Total = item.Total ?? 0,
                        Cost = item.PurchasePrise ?? 0,
                        Batch = item.Wajba,
                        Area = areaInfo != null ? (double?)areaInfo.Mkaab ?? 0 : 0,
                        Weight = areaInfo?.Mkaab ?? 0
                    };
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetMultipleInvoicesItems for IDs: {InvoiceIds}",
                    string.Join(", ", invoiceIds));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }

}
