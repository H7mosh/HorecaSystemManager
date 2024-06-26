using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;

using sacmy.Shared.ViewModels.InvoiceViewModel;
using sacmy.Shared.ViewModels.TrackViewModel;

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
            var invoiceList = await _context.BuyFatoras.Select(e => new InvoiceViewModel
            {
                Id = e.Id,
                CustomerName = e.Customer,
                CustomerType = e.CostType,
                Address = e.Address,
                InvoiceBranch = e.Subb,
                Total = Convert.ToDouble(e.Tootal.ToString()),
                DateTime = e.Now ?? DateTime.Today
            }).OrderByDescending(e => e.Id).ToListAsync();

            return Ok(invoiceList);
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
                    .Include(e => e.Tracks)
                    .ThenInclude(t => t.Type)
                    .Include(c => c.Tracks)
                    .ThenInclude(t => t.TrackComments)
                    .ThenInclude(tc => tc.TrackCommentStates)
                    .ThenInclude(st => st.State)
                    .ToListAsync();

                var invoiceViewModels = new List<InvoiceViewModel>();

                foreach (var invoice in invoices)
                {
                    var lastTrack = invoice.Tracks.OrderByDescending(t => t.CreatedDate).FirstOrDefault();
                    var lastTrackComment = lastTrack?.TrackComments.OrderByDescending(tc => tc.CreatedDate).FirstOrDefault();
                    var lastTrackCommentState = lastTrackComment?.TrackCommentStates.OrderByDescending(tcs => tcs.Id).FirstOrDefault();
                    var state = lastTrackCommentState?.State?.StateAr;

                    invoiceViewModels.Add(new InvoiceViewModel
                    {
                        Id = invoice.Id,
                        CustomerName = invoice.Customer,
                        CustomerType = invoice.CostType,
                        Address = invoice.Address,
                        InvoiceBranch = invoice.Subb,
                        Total = Convert.ToDouble(invoice.Tootal.ToString()),
                        DateTime = invoice.Now ?? DateTime.Today,
                        TrackId = lastTrack?.Id,
                        TrackType = lastTrack?.Type?.TypeAr,
                        State = state
                    });
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
