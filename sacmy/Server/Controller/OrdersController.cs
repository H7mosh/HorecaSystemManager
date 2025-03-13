using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.InvoiceViewModel;
using sacmy.Shared.ViewModels.OrdersViewModel;
using sacmy.Shared.ViewModels.StickNoteViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        private readonly ILogger<OrdersController> _logger;
        int total_points = 0;
        BuyFatoraItem buyfatoraItem = new BuyFatoraItem();
        BuyFatora buyFatora = new BuyFatora();
        EventCostumer eventCostumer = new EventCostumer();
        UnavilableOrderedItem unavilableOrderedItem = new UnavilableOrderedItem();
        CustomerBillPoint customerBill = new CustomerBillPoint();
        private readonly ReportService _reportService;

        public OrdersController(SafeenCompanyDbContext context, ILogger<OrdersController> logger, ReportService reportService)
        {
            _context = context;
            _logger = logger;
            _reportService = reportService;
        }

        [HttpGet("stages")]
        public async Task<ActionResult<List<OrdersByStagesViewModel>>> GetStages()
        {
            try
            {
                var stages = await _context.OrderStages
                    .Select(stage => new OrdersByStagesViewModel
                    {
                        OrderStageId = stage.Id,
                        OrderStageNameEn = stage.StageNameEn,
                        OrderStageNameAr = stage.StageNameAr,
                        OrderStageNameTr = stage.StageNameTr,
                        OrderStageNameKr = stage.StageNameKr,
                        Sequence = stage.Sequence,
                        OrderCount = stage.OrderTrackings.Count()
                    })
                    .ToListAsync();

                _logger.LogInformation($"Retrieved {stages.Count} stages");
                return Ok(stages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving stages");
                return StatusCode(500, "Internal server error while retrieving stages");
            }
        }

        [HttpGet("orders-by-stage")]
        public async Task<ActionResult<PaginatedResponse<OrderViewModel>>> GetOrdersByStage([FromQuery] PaginationFilter filter)
        {
            try
            {
                var query = _context.OrderTrackings
                    .Include(e => e.OrderTrackingInvoices)
                    .Where(ot => ot.Order != null);

                // Apply filters
                if (filter.OrderStageId.HasValue)
                {
                    query = query.Where(ot => ot.StageId == filter.OrderStageId);
                }

                if (!string.IsNullOrEmpty(filter.SearchTerm))
                {
                    var searchTerm = filter.SearchTerm.ToLower();
                    query = query.Where(ot =>
                        ot.Order.CostumerName.ToLower().Contains(searchTerm) ||
                        ot.Order.OrderId.ToString().Contains(searchTerm) ||
                        ot.Order.Address.ToLower().Contains(searchTerm));
                }

                if (filter.StartDate.HasValue)
                {
                    query = query.Where(ot => ot.Order.CreatedDate >= filter.StartDate.Value.Date);
                }

                if (filter.EndDate.HasValue)
                {
                    query = query.Where(ot => ot.Order.CreatedDate <= filter.EndDate.Value.Date);
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync();

                // Apply pagination and fetch data
                var ordersData = await query
                    .Select(ot => new OrderViewModel
                    {
                        OrderId = ot.Order.OrderId,
                        CustomerId = (int)ot.Order.CustomerId,
                        CostumerName = ot.Order.CostumerName,
                        CostumerFirebaseToken = ot.Order.Customer.FirebaseToken,
                        Address = ot.Order.Address,
                        ItemCount = ot.Order.OnlineOrderItems.Count(),
                        Total = ot.Order.OnlineOrderItems.Sum(i => i.Total ?? 0),
                        CreatedDate = (DateTimeOffset)ot.Order.CreatedDate,
                        Invoices = ot.OrderTrackingInvoices
                            .OrderBy(oti => oti.BuyFatora.Date)
                            .Select(oti => new BuyFatoraViewModel
                            {
                                Id = oti.BuyFatora.Id,
                                Customer = oti.BuyFatora.Customer,
                                CustomerId = ot.Order.CustomerId.ToString(),
                                CustomerFirebaseToken = ot.Order.Customer.FirebaseToken,
                                CustomerAddress = oti.BuyFatora.Address,
                                
                                Date = oti.BuyFatora.Date,
                                Payed = oti.BuyFatora.Payed,
                                Remaing = oti.BuyFatora.Remaing,
                                Tootal = oti.BuyFatora.Tootal,
                                IsPdfGenerated = oti.IsPdfGenerated,
                                branch = oti.BuyFatora.Subb
                            }).ToList(),
                    })
                    .ToListAsync();

                var allOrderStickyNotes = await _context.StickyNotes
                    .Include(n => n.Employee)
                    .Where(n => n.TableName == "Orders")
                    .OrderByDescending(n => n.CreatedDate)
                    .ToListAsync();


                var orderIdStrings = ordersData.Select(o => o.OrderId.ToString()).ToList();
                var stickyNotes = allOrderStickyNotes
                    .Where(n => orderIdStrings.Contains(n.RecordId))
                    .Select(note => new {
                        OrderId = note.RecordId,
                        Note = new GetStickyNoteViewModel
                        {
                            Id = note.Id,
                            TableName = note.TableName,
                            RecordId = note.RecordId,
                            EmployeeId = note.EmployeeId,
                            Note = note.Note,
                            CreatedDate = note.CreatedDate,
                            Employee = new GetEmployeeViewModel
                            {
                                Id = note.Employee.Id,
                                FirstName = note.Employee.FirstName,
                                LastName = note.Employee.LastName,
                                Image = note.Employee.Image,
                                JobTitle = note.Employee.JobTitle,
                                Branch = note.Employee.Branch
                            }
                        }
                    })
                    .ToList();

                // Group sticky notes by order ID and assign to orders
                var notesByOrder = stickyNotes.GroupBy(n => n.OrderId)
                    .ToDictionary(g => g.Key, g => g.Select(n => n.Note).ToList());

                // Assign sticky notes to each order
                foreach (var order in ordersData)
                {
                    if (notesByOrder.TryGetValue(order.OrderId.ToString(), out var notes))
                    {
                        order.StickyNotes = notes;
                    }
                }

                foreach (var order in ordersData)
                {
                    if (order.Invoices != null && order.Invoices.Any())
                    {
                        order.Invoices[0].IsItTheMainOne = true; // Set true for the first invoice
                    }
                }

                // Wrap response in pagination format
                var response = new PaginatedResponse<OrderViewModel>(
                    ordersData, totalCount, filter.PageNumber, filter.PageSize);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders");
                return StatusCode(500, "Internal server error while retrieving orders");
            }
        }

        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<List<OrderItemViewModel>>> GetOrderItems(int orderId)
        {
            try
            {
                var orderItems = await _context.OnlineOrderItems
                    .Where(oi => oi.OrderId == orderId)
                    .Select(oi => new OrderItemViewModel
                    {
                        ItemId = oi.ItemId,
                        Sku = oi.Item1.Sku,
                        PatternCode = oi.Item1.PatternNumber,
                        Quantity = oi.Qtty ?? 0,
                        Price = oi.Price ?? 0,
                        Total = oi.Total ?? 0,
                        Point = oi.Item1.Points ?? 0,
                    })
                    .ToListAsync();

                if (!orderItems.Any())
                {
                    _logger.LogWarning($"No items found for order {orderId}");
                    return NotFound($"No items found for order {orderId}");
                }

                _logger.LogInformation($"Retrieved {orderItems.Count} items for order {orderId}");
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving items for order {orderId}");
                return StatusCode(500, "Internal server error while retrieving order items");
            }
        }

        [HttpPost("filterOrder")]
        public async Task<IActionResult> FilterOrders([FromBody] OrderToFilter orderToFilter)
        {
            if (orderToFilter == null || orderToFilter.onlineOrderItemsToFilter == null || orderToFilter.storageOrderList == null)
            {
                return BadRequest("Invalid input data.");
            }

            var resultsByBranch = new Dictionary<string, List<OrderViewerViewModel>>();
            // Initialize the "undefined" key with an empty list at the start
            resultsByBranch["undefined"] = new List<OrderViewerViewModel>();

            // Store the original order of branches
            var branchOrder = orderToFilter.storageOrderList
                .Select(s => s.Name)
                .ToList();

            foreach (var item in orderToFilter.onlineOrderItemsToFilter)
            {
                double remainingQuantity = item.Qtty.GetValueOrDefault();
                bool itemFound = false;

                foreach (var storageOrder in orderToFilter.storageOrderList.OrderBy(s => s.order))
                {
                    var matchingItems = await _context.MmMaxzanFullItemProcWithWajbas
                        .Where(w => w.Cod == item.Sku && w.Subb == storageOrder.Name && w.Remain > 0)
                        .OrderBy(w => w.Date)
                        .ToListAsync();

                    if (matchingItems.Any())
                    {
                        foreach (var matchingItem in matchingItems)
                        {
                            double quantityToUse = Math.Min(remainingQuantity, matchingItem.Remain.GetValueOrDefault());
                            if (!resultsByBranch.ContainsKey(matchingItem.Subb))
                            {
                                resultsByBranch[matchingItem.Subb] = new List<OrderViewerViewModel>();
                            }

                            var newItem = new OrderViewerViewModel
                            {
                                Id = Guid.NewGuid(),
                                OrderId = item.OrderId,
                                Secode = matchingItem.Secode,
                                PattrenNumber = matchingItem.CodIqd,
                                Batch = matchingItem.Wajba,
                                ItemName = item.Item,
                                Sku = matchingItem.Cod,
                                Quantity = quantityToUse,
                                Cost = matchingItem.AvrgPrice.GetValueOrDefault().ToString(),
                                Price = matchingItem.Prise.GetValueOrDefault().ToString(),
                                Branch = matchingItem.Subb,
                                IsAvailable = true
                            };

                            resultsByBranch[matchingItem.Subb].Add(newItem);
                            remainingQuantity -= quantityToUse;

                            if (remainingQuantity <= 0)
                            {
                                itemFound = true;
                                break;
                            }
                        }
                    }

                    if (itemFound)
                    {
                        break;
                    }
                }

                // If the item wasn't found or there's still remaining quantity, add it to undefined
                if (!itemFound || remainingQuantity > 0)
                {
                    var unavailableItem = new OrderViewerViewModel
                    {
                        Id = Guid.NewGuid(),
                        ItemName = item.Item,
                        OrderId = item.OrderId,
                        Sku = item.Sku,
                        PattrenNumber = "undefined",
                        Secode = 0,
                        Batch = "undefined",
                        Quantity = remainingQuantity > 0 ? remainingQuantity : item.Qtty.GetValueOrDefault(),
                        Price = item.Price.GetValueOrDefault().ToString(),
                        Cost = "0",
                        Branch = "undefined",
                        IsAvailable = false
                    };
                    resultsByBranch["undefined"].Add(unavailableItem);
                }
            }

            // Remove any empty branches
            var emptyBranches = resultsByBranch.Where(kvp => !kvp.Value.Any()).Select(kvp => kvp.Key).ToList();
            foreach (var branch in emptyBranches)
            {
                resultsByBranch.Remove(branch);
            }

            // Create ordered results dictionary
            var orderedResults = new Dictionary<string, List<OrderViewerViewModel>>();

            // Add undefined first if it has any items
            if (resultsByBranch.ContainsKey("undefined") && resultsByBranch["undefined"].Any())
            {
                orderedResults["undefined"] = resultsByBranch["undefined"];
            }

            // Add other branches in the original order
            foreach (var branchName in branchOrder)
            {
                if (resultsByBranch.ContainsKey(branchName) && resultsByBranch[branchName].Any())
                {
                    orderedResults[branchName] = resultsByBranch[branchName];
                }
            }

            return Ok(orderedResults);
        }

        [HttpPost("createinvoice")]
        public async Task<IActionResult> PostFatora(InvoiceFromOrderViewModel fatoraViewModel)
        {
            try
            {
                int total_points = 0;
                BuyFatora buyFatora = new BuyFatora();
                EventCostumer eventCostumer = new EventCostumer();
                CustomerBillPoint customerBill = new CustomerBillPoint();
                OnlineOrder online_order = await _context.OnlineOrders.FindAsync(fatoraViewModel.OrderId);

                foreach (var fatoraItem in fatoraViewModel.invoiceFromOrderItemsViewModel)
                {
                    KpStore item1 = await _context.KpStores.FirstOrDefaultAsync(e => e.Sku == fatoraItem.Sku);
                    if (item1 != null)
                    {
                        total_points += (int)item1.Points * (int)fatoraItem.Quantity;
                    }
                }

                Customer customer = await _context.Customers.FindAsync(fatoraViewModel.CustomerId);

                if (customer == null)
                {
                    return NotFound($"Customer with ID {fatoraViewModel.CustomerId} not found.");
                }


                int lastId = await _context.BuyFatoras.OrderByDescending(f => f.Id).Select(f => f.Id).FirstOrDefaultAsync();
                buyFatora.Id = lastId + 10;

                if (fatoraViewModel.Sub != "undefined")
                {

                    buyFatora.Idd = 0;
                    buyFatora.Date = fatoraViewModel.FatoraDate;
                    buyFatora.Datee = fatoraViewModel.FatoraDate;
                    buyFatora.Mob = null;
                    buyFatora.Dolar = 0;
                    buyFatora.Now = fatoraViewModel.FatoraDate;
                    buyFatora.Treasurer = "Treasurer";
                    buyFatora.Uuser = fatoraViewModel.UserId;
                    buyFatora.Discount = 0;
                    buyFatora.Mandob = string.Empty;
                    buyFatora.ManCcount = 0;
                    buyFatora.Ijraaa = 0;
                    buyFatora.Ijraaa2 = 0;
                    buyFatora.Driver = null;
                    buyFatora.CarNo = null;
                    buyFatora.DriverMob = null;
                    buyFatora.EventId = "BU - " + buyFatora.Id;
                    buyFatora.CostType = "Normal";
                    buyFatora.Subb = fatoraViewModel.Sub;
                    buyFatora.Customer = customer.Customer1;
                    buyFatora.Address = customer.Address;
                    buyFatora.Nsba = 0;
                    buyFatora.Nsbaother = 0;
                    buyFatora.TotalPoints = total_points;
                    buyFatora.Payed = (decimal?)fatoraViewModel.Payed;
                    buyFatora.Remaing = (decimal?)fatoraViewModel.Remain;
                    buyFatora.Tootal = (decimal?)fatoraViewModel.Total;
                    buyFatora.Checkeed = true;
                    buyFatora.Hamalya = (decimal?)fatoraViewModel.Hamalya;
                    buyFatora.Notes = fatoraViewModel.Note;
                    buyFatora.EditedDate = null;
                    buyFatora.EditedUser = null;
                    buyFatora.Hidee = false;
                    buyFatora.NotifyMe = false;
                    buyFatora.NoteOther = null;
                    buyFatora.PaymentDate = null;
                    buyFatora.Sender = null;
                    buyFatora.Bankinfo = null;

                    _context.BuyFatoras.Add(buyFatora);
                }

                foreach (var fatoraItem in fatoraViewModel.invoiceFromOrderItemsViewModel)
                {
                    if (fatoraItem.Wajba == "undefined")
                    {
                        Item1 item = await _context.Items1.FirstOrDefaultAsync(e => e.Sku == fatoraItem.Sku);
                        if (item != null)
                        {
                            UnavilableOrderedItem unavilableOrderedItem = new UnavilableOrderedItem
                            {
                                Id = Guid.NewGuid(),
                                Sku = fatoraItem.Sku,
                                PattrenCode = item.PattrenNo,
                                Quantity = (int)fatoraItem.Quantity,
                                ItemId = fatoraItem.ItemId,
                                Secode = fatoraItem.Secode,
                                BillId = buyFatora.Id
                            };
                            _context.UnavilableOrderedItems.Add(unavilableOrderedItem);
                        }
                    }
                    else
                    {
                        KpStore item = await _context.KpStores.FirstOrDefaultAsync(e => e.Sku == fatoraItem.Sku);
                        if (item != null)
                        {
                            QqMaxzanFullItemProc qqMaxzanFullItemProc = await _context.QqMaxzanFullItemProcs.FirstOrDefaultAsync(e => e.Secode == fatoraItem.Secode);
                            OnlineOrder onlineOrder = await _context.OnlineOrders.FindAsync(fatoraItem.OrderItemId);
                            MmMaxzanByWajbaFullItem mmMaxzanByWajbaFullItem = await _context.MmMaxzanByWajbaFullItems.FirstOrDefaultAsync(e => e.Secode == fatoraItem.Secode && e.Wajba == fatoraItem.Wajba);

                            BuyFatoraItem buyFatoraItem = new BuyFatoraItem
                            {
                                Id = buyFatora.Id,
                                BuId = 0,
                                Codd = item!.Sku,
                                Typee = "زجاجيات",
                                Factoryy = "Pasabhace",
                                QiyasUnit = "كارتون",
                                Countt = 0,
                                Prise = (decimal)fatoraItem!.Price,
                                CodIqd = item!.PatternNumber,
                                BoxContain = item!.InnerTypeCount.ToString() + "*" + item.OuterTypeCount,
                                Itemm = mmMaxzanByWajbaFullItem.Item,
                                Secode = fatoraItem.Secode,
                                Points = item!.Points,
                                Quantity = fatoraItem.Quantity,
                                Total = (decimal)(double.Parse(fatoraItem.Quantity.ToString()) * fatoraItem!.Price),
                                Wajba = fatoraItem?.Wajba,
                                Subb = fatoraItem?.Storage,
                                PurchasePrise = (decimal)qqMaxzanFullItemProc!.AvrgPrice,
                                QttRemaining = 0,
                                Rub7Karton = 0,
                                Weznn = 0,
                                IsDeleted = false,
                                TtalPoints = (int)(item!.Points * fatoraItem.Quantity)
                            };

                            _context.BuyFatoraItems.Add(buyFatoraItem);

                            if (onlineOrder != null)
                            {
                                onlineOrder.Checked = false;
                            }
                        }
                    }
                }


                if (fatoraViewModel.Sub != "undefined")
                {
                    eventCostumer.IdEvent = 0;
                    eventCostumer.Costumer = customer.Customer1;
                    eventCostumer.EventId = "BU - " + buyFatora.Id;
                    eventCostumer.Trans = 1;
                    eventCostumer.Ttttotal = buyFatora.Remaing;
                    eventCostumer.Datee = fatoraViewModel.FatoraDate;
                    eventCostumer.Typeevent = "فاتورة ألبيع";
                    eventCostumer.Noww = fatoraViewModel.FatoraDate;
                    eventCostumer.Subb = fatoraViewModel.invoiceFromOrderItemsViewModel?[0].Storage;
                    _context.EventCostumers.Add(eventCostumer);
                }


                await _context.SaveChangesAsync();

                if (fatoraViewModel.Sub != "undefined")
                {
                    int orderId = fatoraViewModel.OrderId;
                    int invoiceId = buyFatora.Id;


                    var existingOrderTracking = await _context.OrderTrackings
                        .FirstOrDefaultAsync(ot => ot.OrderId == orderId);

                    Guid invoiceFinalizedandSentGuid = Guid.Parse("480140e5-35db-4887-b8f9-1eb5ba2977e9");
                    if (existingOrderTracking == null)
                    {
                        existingOrderTracking = new OrderTracking
                        {
                            Id = Guid.NewGuid(),
                            OrderId = orderId,
                            StageId = invoiceFinalizedandSentGuid,
                            IsCompleted = false,
                            CreatedDate = DateTime.UtcNow
                        };

                        _context.OrderTrackings.Add(existingOrderTracking);
                    }

                    existingOrderTracking.StageId = invoiceFinalizedandSentGuid;

                    await _context.SaveChangesAsync();

                    var existingOrderTrackingInvoice = await _context.OrderTrackingInvoices
                        .FirstOrDefaultAsync(oti => oti.OrderTrackingId == existingOrderTracking.Id && oti.BuyFatoraId == invoiceId);

                    if (existingOrderTrackingInvoice == null)
                    {
                        OrderTrackingInvoice orderTrackingInvoice = new OrderTrackingInvoice
                        {
                            Id = Guid.NewGuid(),
                            OrderTrackingId = existingOrderTracking.Id,
                            BuyFatoraId = invoiceId,
                            CreatedDate = DateTime.UtcNow
                        };

                        _context.OrderTrackingInvoices.Add(orderTrackingInvoice);
                    }


                    await _context.SaveChangesAsync();
                }

                return Ok(fatoraViewModel);

            }
            catch (Exception ex)
            {
                // Log the exception details and return an internal server error message
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpPost("update-order-stage")]
        public async Task<IActionResult> UpdateOrderStage(int orderId, Guid stageId, int invoiceId, bool isItTheMainInvoice)
        {
            try
            {
                var orderTracking = await _context.OrderTrackings
                                    .Include(e => e.OrderTrackingInvoices)
                                    .FirstOrDefaultAsync(ot => ot.OrderId == orderId);
                if (orderTracking == null)
                {
                    return NotFound($"Order tracking record not found for OrderId: {orderId}");
                }

                if (isItTheMainInvoice)
                {
                    // Update the StageId
                    orderTracking.StageId = stageId;
                }

                var OrderTrackingInvoices = orderTracking.OrderTrackingInvoices.FirstOrDefault(e => e.BuyFatoraId == invoiceId);

                OrderTrackingInvoices.IsPdfGenerated = true;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Order stage updated successfully",
                    OrderId = orderId,
                    StageId = stageId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating order stage for OrderId: {orderId}");
                return StatusCode(500, "Internal server error while updating order stage");
            }
        }

        [HttpPost("upload-invoice-attachment")]
        public async Task<IActionResult> UploadInvoiceAttachment([FromForm] int orderId, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file was uploaded.");

                // Find the order tracking
                var orderTracking = await _context.OrderTrackings
                    .FirstOrDefaultAsync(ot => ot.OrderId == orderId);

                if (orderTracking == null)
                    return NotFound($"Order tracking not found for order ID: {orderId}");

                // Create file service instance
                var fileService = new FileService();

                // Set the fixed upload directory path
                var uploadDirectory = @"C:\assets\Invoices";

                // Ensure directory exists
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Upload the file
                string fileName = await fileService.UploadFileAsync(
                    file,
                    $"Order_{orderId}_Invoice",
                    uploadDirectory);

                // Generate full file path
                string fullFilePath = Path.Combine(uploadDirectory, fileName);

                // Create new OrderTrackingAttachment
                var attachment = new OrderTrackingAttachment
                {
                    Id = Guid.NewGuid(),
                    OrderTrackingId = orderTracking.Id,
                    Attachment = "https://api.safinahmedtech.com/assets/Invoices/" + fileName,  // Store the full file path instead of just the filename
                    CreatedDate = DateTime.Now
                };

                // Add attachment to database
                await _context.OrderTrackingAttachments.AddAsync(attachment);

                // Update order tracking stage
                orderTracking.StageId = Guid.Parse("FB10171A-21CD-4D8D-8453-0AC58D31D95F");

                // Save changes
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Invoice attachment uploaded successfully",
                    OrderId = orderId,
                    AttachmentId = attachment.Id,
                    FileName = fileName,
                    FilePath = fullFilePath
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading invoice attachment for OrderId: {orderId}");
                return StatusCode(500, "Internal server error while uploading invoice attachment");
            }
        }

        [HttpPost("generate-invoice-pdf")]
        public IActionResult GenerateInvoicePdf([FromBody] InvoicePdfRequest request)
        {
            _logger.LogInformation("GenerateInvoicePdf endpoint hit");

            try
            {
                _logger.LogInformation("Request received: {@Request}", request);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model state is invalid: {@ModelState}", ModelState);
                    return BadRequest(ModelState);
                }

                var pdfBytes = _reportService.GenerateInvoiceReport(request.Invoice, request.InvoiceItems);
                return File(pdfBytes, "application/pdf", $"Invoice_{request.Invoice.Id}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating invoice PDF");
                return StatusCode(500, "Error generating invoice PDF");
            }
        }

        [HttpGet("generate-invoice-report/{invoiceId}")]
        public async Task<ActionResult<InvoiceViewModel>> GenerateInvoiceReport(int invoiceId)
        {
            _logger.LogInformation("GenerateInvoiceReport endpoint hit for invoiceId: {invoiceId}", invoiceId);
            try
            {
                var invoice = await _context.BuyFatoras
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Type)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.TaskNotes)
                    .Include(c => c.Tasks)
                    .ThenInclude(t => t.Status)
                    .FirstOrDefaultAsync(e => e.Id == invoiceId);

                if (invoice == null)
                {
                    return NotFound($"Invoice with ID {invoiceId} not found");
                }

                invoice.IsPdfGenerated = true;
                await _context.SaveChangesAsync();

                var lastTask = invoice.Tasks?.LastOrDefault();
                var invoiceViewModel = new InvoiceViewModel
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
                    TaskId = lastTask?.Id,
                    TaskStatus = lastTask?.Status?.StateEn,
                    LastComment = lastTask?.TaskNotes?.LastOrDefault()?.Note
                };

                return Ok(invoiceViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating invoice PDF for invoiceId: {invoiceId}", invoiceId);
                return StatusCode(500, "Error generating invoice PDF");
            }
        }
    }
}
