using DocumentFormat.OpenXml.Office2019.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.Models;
using sacmy.Shared.ViewModels.CustomerTracker;
using sacmy.Shared.ViewModels.TrackViewModel;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.StickNoteViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTrackerController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        private readonly ILogger<InvoiceController> _logger;

        public CustomerTrackerController(SafeenCompanyDbContext context, ILogger<InvoiceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrack([FromQuery] Guid userId)
        {
            try
            {
                var trackList = await _context.Tracks
                                .Include(e => e.Employe)
                                .Include(e => e.Type)
                                .Include(e => e.Customer)
                                .Include(e => e.TrackComments)
                                .ThenInclude(e => e.TrackCommentStates)
                                .Select(
                    e => new GetTrackViewModel
                    {
                        Id = e.Id,
                        Type = e.Type.TypeAr,
                        CustomerName = e.Customer.Customer1,
                        EmployeeId = e.Employe.Id,
                        EmployeeName = e.Employe.FirstName + " " + e.Employe.LastName,
                        AssignedToId = e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().AssignedTo,
                        AssignedToName = e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().AssignedToNavigation.FirstName + " " + e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().AssignedToNavigation.LastName,
                        State = e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().TrackCommentStates.FirstOrDefault().State.StateAr,
                        ReschedueldAt = e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().ReOpenAt,
                        Category = (e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().AssignedTo == userId) ? "ForYou" :
                                   (e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().ReOpenAt.HasValue && e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().ReOpenAt.Value.Date == DateTime.Today) ? "Today" :
                                   (e.TrackComments.OrderBy(x => x.CreatedDate).LastOrDefault().TrackCommentStates.FirstOrDefault().State.StateAr == "مفتوحه") ? "Open" :
                                   "All"
                    }
                ).ToListAsync();

                return Ok(trackList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetCustomerTrack")]
        public async Task<IActionResult> GetCustomerTrack([FromQuery] int customerId)
        {
            try
            {
                var list = await _context.Tracks.
                                 Include(e => e.Employe)
                                .Include(e => e.Type)
                                .Include(e => e.Customer)
                                .Include(e => e.TrackComments)
                                .ThenInclude(e => e.TrackCommentStates)
                                .Where(e => e.CustomerId == customerId)
                                .Select(e => new GetCustomerTrackViewModel
                                {
                                    CustomerId = e.CustomerId ?? 0,
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

        [HttpGet("GetCostumerRemainTotal")]
        public async Task<IActionResult> GetCostumerTotalRemain([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15, [FromQuery] string searchTerm = null)
        {
            try
            {
                // Get all the basic data needed with minimal LINQ complexity
                // 1. Get all customers without filters to avoid EF translation errors
                var allCustomers = await _context.Customers.ToListAsync();

                // 2. Get all tasks without filters to avoid EF translation errors
                var allTasks = await _context.Tasks
                    .Include(t => t.Type)
                    .Include(t => t.Status)
                    .Include(t => t.TaskNotes)
                    .ToListAsync();

                // 3. Get all running totals without filters to avoid EF translation errors
                var allRunningTotals = await _context.QqqCountCostumerNewTRunings.ToListAsync();

                // Do all filtering and processing in memory
                // 1. Filter customers with non-empty names
                var validCustomers = allCustomers
                    .Where(c => !string.IsNullOrEmpty(c.Customer1))
                    .ToList();

                // 2. Group tasks by customer ID for easier lookup
                var tasksByCustomer = allTasks
                    .Where(t => t.CustomerId.HasValue)
                    .GroupBy(t => t.CustomerId.Value)
                    .ToDictionary(g => g.Key, g => g.OrderByDescending(t => t.CreatedDate).ToList());

                // 3. Calculate date threshold for recent receipts (45 days ago)
                var dateThreshold = DateTime.Now.AddDays(-45);

                // 4. Find customers with recent receipts
                var customersWithRecentReceipt = allRunningTotals
                    .Where(q =>
                        (q.EventId != null && (
                            q.EventId.Contains("pfc", StringComparison.OrdinalIgnoreCase) ||
                            q.EventId.Contains("pf", StringComparison.OrdinalIgnoreCase))
                        ) &&
                        q.Datee.HasValue && q.Datee.Value >= dateThreshold
                    )
                    .Select(q => q.Costumer)
                    .Distinct()
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // 5. Group running totals by customer name
                var customerGroups = allRunningTotals
                    .Where(q => q.Costumer != null)
                    .GroupBy(q => q.Costumer)
                    .ToList();

                // Process all data in memory
                var deptCustomerViewModels = new List<DeptCustomerViewModel>();

                foreach (var group in customerGroups)
                {
                    var customerName = group.Key;
                    // Find matching customer by name (case-insensitive)
                    var customer = validCustomers.FirstOrDefault(c =>
                        string.Equals(c.Customer1, customerName, StringComparison.OrdinalIgnoreCase));

                    if (customer == null)
                        continue;

                    // Calculate total for this customer
                    decimal totalTransTotalN = 0;
                    foreach (var item in group)
                    {
                        totalTransTotalN += item.TransTotalN ?? 0;
                    }

                    // Skip customers with total <= 200
                    if (totalTransTotalN <= 200)
                        continue;

                    // Determine if customer has recent receipt
                    string hasRecentReceipt = customersWithRecentReceipt.Contains(customerName) ? "نعم" : "لا";

                    // Find last task for this customer if any
                    List<sacmy.Server.Models.Task> customerTasks = null;
                    tasksByCustomer.TryGetValue(customer.Id, out customerTasks);

                    var lastTask = customerTasks?.FirstOrDefault();

                    if (lastTask != null)
                    {
                        var taskStatus = lastTask.Status?.StateEn;
                        var lastTaskNote = lastTask.TaskNotes?
                            .OrderByDescending(n => n.CreatedDate)
                            .FirstOrDefault();

                        var lastComment = lastTaskNote?.Note;

                        deptCustomerViewModels.Add(new DeptCustomerViewModel
                        {
                            Id = customer.Id,
                            CustomerName = customerName,
                            TotalTransTotalN = totalTransTotalN,
                            HasRecentReceipt = hasRecentReceipt,
                            TaskId = lastTask.Id,
                            TaskStatus = taskStatus,
                            LastComment = lastComment
                        });
                    }
                    else
                    {
                        deptCustomerViewModels.Add(new DeptCustomerViewModel
                        {
                            Id = customer.Id,
                            CustomerName = customerName,
                            TotalTransTotalN = totalTransTotalN,
                            HasRecentReceipt = hasRecentReceipt,
                            TaskId = null,
                            TaskStatus = null,
                            LastComment = null
                        });
                    }
                }

                // Apply search term filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    deptCustomerViewModels = deptCustomerViewModels.Where(c =>
                        (c.CustomerName != null && c.CustomerName.ToLower().Contains(searchTerm)) ||
                        (c.TaskStatus != null && c.TaskStatus.ToLower().Contains(searchTerm)) ||
                        (c.LastComment != null && c.LastComment.ToLower().Contains(searchTerm)) ||
                        (c.HasRecentReceipt != null && c.HasRecentReceipt.ToLower().Contains(searchTerm))
                    ).ToList();
                }

                // Get total count for pagination
                var totalCount = deptCustomerViewModels.Count;

                // Apply pagination
                var paginatedResult = deptCustomerViewModels
                    .OrderByDescending(c => c.TotalTransTotalN)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

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

                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHiddenCustomer")]
        public async Task<ActionResult<List<CustomerHiddenViewModel>>> GetHidderCustomer([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 15,[FromQuery] string searchTerm = null,[FromQuery] string dateFilter = "default") 
        {
            try
            {
                // Determine threshold date based on date filter parameter
                DateTime thresholdDate;

                switch (dateFilter?.ToLower())
                {
                    case "1month":
                        thresholdDate = DateTime.Now.AddMonths(-1);
                        break;
                    case "3months":
                        thresholdDate = DateTime.Now.AddMonths(-3);
                        break;
                    case "6months":
                        thresholdDate = DateTime.Now.AddMonths(-6);
                        break;
                    case "1year":
                        thresholdDate = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        // Default is 25 days (as per original implementation)
                        thresholdDate = DateTime.Now.AddDays(-25);
                        break;
                }

                // Get all customers first, then filter in memory
                var allCustomers = await _context.Customers
                    .Include(c => c.Tasks)
                        .ThenInclude(t => t.Type)
                    .Include(c => c.Tasks)
                        .ThenInclude(t => t.TaskNotes)
                    .Include(c => c.Tasks)
                        .ThenInclude(t => t.Status)
                    .ToListAsync();

                // Get distinct customers by name
                var distinctCustomers = allCustomers
                    .GroupBy(c => c.Customer1)
                    .Select(g => g.First())
                    .ToList();

                // Get all invoices
                var invoices = await _context.BuyFatoras.ToListAsync();

                // Get all sticky notes for customers
                var customerIds = distinctCustomers.Select(c => c.Id.ToString()).ToList();
                var allStickyNotes = await _context.StickyNotes
                    .Include(sn => sn.Employee)
                    .Where(sn => sn.TableName == "Customers")
                    .ToListAsync();

                // Filter sticky notes in memory
                var relevantStickyNotes = allStickyNotes
                    .Where(sn => customerIds.Contains(sn.RecordId))
                    .ToList();

                // Group sticky notes by customer ID
                var stickyNotesGrouped = relevantStickyNotes
                    .GroupBy(sn => sn.RecordId)
                    .ToDictionary(g => g.Key, g => g.OrderByDescending(n => n.CreatedDate).ToList());

                var customerViewModels = new List<CustomerHiddenViewModel>();

                foreach (var customer in distinctCustomers)
                {
                    var lastInvoice = invoices
                        .Where(i => i.Customer == customer.Customer1)
                        .OrderByDescending(i => i.Datee)
                        .FirstOrDefault();

                    // Apply date filter: Only include customers whose last invoice date is before the threshold date
                    if (lastInvoice != null && lastInvoice.Datee < thresholdDate)
                    {
                        var lastTask = customer.Tasks?.OrderByDescending(t => t.CreatedDate).FirstOrDefault();

                        // Look for sticky notes for this customer
                        stickyNotesGrouped.TryGetValue(customer.Id.ToString(), out var customerNotes);

                        // Calculate days since last invoice
                        int daysSinceLastInvoice = 0;
                        if (lastInvoice.Datee.HasValue)
                        {
                            daysSinceLastInvoice = (DateTime.Now - lastInvoice.Datee.Value).Days;
                        }

                        CustomerHiddenViewModel viewModel;
                        if (lastTask != null)
                        {
                            var taskType = lastTask.Type?.TypeAr;
                            var taskStatus = lastTask.Status?.StateEn;
                            var lastTaskComment = lastTask.TaskNotes?.OrderByDescending(n => n.CreatedDate).FirstOrDefault()?.Note;

                            viewModel = new CustomerHiddenViewModel
                            {
                                Id = customer.Id,
                                Name = customer.Customer1,
                                Location = customer.Address,
                                Type = customer.CostType,
                                LastDate = (DateTime)lastInvoice.Datee,
                                TaskId = lastTask.Id,
                                TaskStatus = taskStatus,
                                LastComment = lastTaskComment,
                                DaysSinceLastInvoice = daysSinceLastInvoice,
                                StickyNotes = customerNotes?.Select(n => new GetStickyNoteViewModel
                                {
                                    Id = n.Id,
                                    TableName = n.TableName,
                                    RecordId = n.RecordId,
                                    EmployeeId = n.EmployeeId,
                                    Note = n.Note,
                                    CreatedDate = n.CreatedDate,
                                    Employee = new GetEmployeeViewModel
                                    {
                                        Id = n.Employee.Id,
                                        FirstName = n.Employee.FirstName,
                                        LastName = n.Employee.LastName,
                                        Image = n.Employee.Image,
                                        JobTitle = n.Employee.JobTitle
                                    }
                                }).ToList() ?? new List<GetStickyNoteViewModel>()
                            };
                        }
                        else
                        {
                            viewModel = new CustomerHiddenViewModel
                            {
                                Id = customer.Id,
                                Name = customer.Customer1,
                                Location = customer.Address,
                                Type = customer.CostType,
                                LastDate = (DateTime)lastInvoice.Datee,
                                DaysSinceLastInvoice = daysSinceLastInvoice,
                                TaskId = null,
                                TaskStatus = null,
                                LastComment = null,
                                StickyNotes = customerNotes?.Select(n => new GetStickyNoteViewModel
                                {
                                    Id = n.Id,
                                    TableName = n.TableName,
                                    RecordId = n.RecordId,
                                    EmployeeId = n.EmployeeId,
                                    Note = n.Note,
                                    CreatedDate = n.CreatedDate,
                                    Employee = new GetEmployeeViewModel
                                    {
                                        Id = n.Employee.Id,
                                        FirstName = n.Employee.FirstName,
                                        LastName = n.Employee.LastName,
                                        Image = n.Employee.Image,
                                        JobTitle = n.Employee.JobTitle
                                    }
                                }).ToList() ?? new List<GetStickyNoteViewModel>()
                            };
                        }

                        customerViewModels.Add(viewModel);
                    }
                }

                // Apply search filtering in memory if needed
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    customerViewModels = customerViewModels.Where(c =>
                        (c.Name != null && c.Name.ToLower().Contains(searchTerm)) ||
                        (c.Location != null && c.Location.ToLower().Contains(searchTerm)) ||
                        (c.Type != null && c.Type.ToLower().Contains(searchTerm)) ||
                        (c.TaskStatus != null && c.TaskStatus.ToLower().Contains(searchTerm)) ||
                        (c.LastComment != null && c.LastComment.ToLower().Contains(searchTerm))
                    ).ToList();
                }

                // Get total count after filtering
                var totalCount = customerViewModels.Count;

                // Apply pagination
                var paginatedResult = customerViewModels
                    .OrderByDescending(c => c.LastDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

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

                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpGet("GetTrackType")]
        public async Task<IActionResult> GetTrackType()
        {
            var track_type_list = await _context.TrackTypes.Select(e => new TrackTypeViewModel { Id = e.Id, TypeEn = e.TypeEn, TypeAr = e.TypeAr, CreatedDate = e.CreatedDate }).ToListAsync();
            return Ok(track_type_list);
        }

        [HttpGet("GetTrackState")]
        public async Task<IActionResult> GetTrackState()
        {
            var track_type_list = await _context.Statuses.Select(e => new CommentTrackStateViewModel { Id = e.Id, StateEn = e.StateEn, StateAr = e.StateAr}).ToListAsync();
            return Ok(track_type_list);
        }

        [HttpPost("AddTrack")]
        public async Task<IActionResult> AddNewTrack(AddTrackViewModel addTrackViewModel)
        {
            Guid TrackId = Guid.NewGuid();
            Guid StateId = addTrackViewModel.StateId;
            Guid TrackCommentId = Guid.NewGuid();


            Models.Track track = new Models.Track
            {
                Id = TrackId,
                CustomerId = addTrackViewModel.CustomerId,
                InvoiceId = addTrackViewModel.InvoiceId,
                TypeId = addTrackViewModel.TypeId,
                Note = addTrackViewModel.Note,
                IsDeleted = false,
                EmployeId = addTrackViewModel.EmplyeeId,
                CreatedDate = DateTime.Now,
            };

            TrackComment trackComment = new TrackComment
            {
                Id = TrackCommentId,
                TrackId = TrackId,
                Comment = addTrackViewModel.Comment,
                AssignedTo = addTrackViewModel.AssignTo,
                ReOpenAt = addTrackViewModel.ReOpenAt,
                EmployeeId = addTrackViewModel.EmplyeeId,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            TrackCommentState trackCommentState = new TrackCommentState
            {
                Id = Guid.NewGuid(),
                StateId = StateId,
                TrackCommentId = TrackCommentId
            };

            try
            {
                await _context.Tracks.AddAsync(track);
                await _context.SaveChangesAsync();
                await _context.TrackComments.AddAsync(trackComment);
                await _context.SaveChangesAsync();
                await _context.TrackCommentStates.AddAsync(trackCommentState);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("GetTrackComments")]
        public async Task<IActionResult> GetTrackComments(Guid TrackId)
        {
            var track = await _context.Tracks
                .Where(e => e.Id == TrackId)
                .Include(e => e.Customer)
                .Include(t => t.Type)
                .Include(t => t.TrackComments)
                .ThenInclude(tc => tc.TrackCommentStates)
                .ThenInclude(tcs => tcs.State)
                .Include(t => t.TrackComments)
                .ThenInclude(tc => tc.Employee) // Include Employee from TrackComment
                .FirstOrDefaultAsync();

            if (track == null)
            {
                return NotFound();
            }
            else
            {
                var trackComments = track.TrackComments.Select(tc => new GetCustomerTrackComment
                {
                    Type = track.Type?.TypeAr ?? "N/A",
                    State = tc.TrackCommentStates.LastOrDefault()?.State?.StateAr ?? "N/A",
                    Comment = tc.Comment ?? "N/A",
                    EmployeeImage = "https://karamanfruit.com/avatar/" + tc.Employee?.Image ?? "N/A",
                    EmployeeJobTitle = tc.Employee?.JobTitle ?? "N/A",
                    EmployeeName = tc.Employee?.FirstName + " " + tc.Employee?.LastName ?? "N/A",
                    CreatedDate = tc.CreatedDate
                }).OrderByDescending(e => e.CreatedDate).ToList();

                return Ok(trackComments);
            }
        }

        [HttpPost("AddTrackStateToExistTrack")]
        public async Task<IActionResult> AddTrackStateToExistTrack(AddCommentToExisingTrack addCommentToExisingTrack)
        {
            Guid trackCommentId = Guid.NewGuid();
            TrackComment trackComment = new TrackComment
            {
                Id = trackCommentId,
                TrackId = addCommentToExisingTrack.TrackId,
                Comment = addCommentToExisingTrack.Comment,
                AssignedTo = addCommentToExisingTrack.AssignTo,
                ReOpenAt = addCommentToExisingTrack.RescheduledAt,
                EmployeeId = addCommentToExisingTrack.EmpId,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };
            TrackCommentState trackCommentState = new TrackCommentState
            {
                Id = Guid.NewGuid(),
                StateId = addCommentToExisingTrack.StateId,
                TrackCommentId = trackCommentId
            };

            try
            {

                await _context.TrackComments.AddAsync(trackComment);
                await _context.SaveChangesAsync();
                await _context.TrackCommentStates.AddAsync(trackCommentState);
                await _context.SaveChangesAsync();


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCustomerStickyNotes")]
        public async Task<IActionResult> GetCustomerStickyNotes([FromQuery] int customerId)
        {
            try
            {
                var stickyNotes = await _context.StickyNotes
                    .Include(sn => sn.Employee)
                    .Where(sn => sn.TableName == "Customers" && sn.RecordId == customerId.ToString())
                    .OrderByDescending(sn => sn.CreatedDate)
                    .Select(note => new GetStickyNoteViewModel
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
                            Branch = note.Employee.Branch,
                            FirebaseToken = note.Employee.FirebaseToken,
                            JobTitle = note.Employee.JobTitle,
                        }
                    })
                    .ToListAsync();

                return Ok(stickyNotes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetInvoiceStickyNotes")]
        public async Task<IActionResult> GetInvoiceStickyNotes([FromQuery] int invoiceId)
        {
            try
            {
                var stickyNotes = await _context.StickyNotes
                    .Include(sn => sn.Employee)
                    .Where(sn => sn.TableName == "Invoices" && sn.RecordId == invoiceId.ToString())
                    .OrderByDescending(sn => sn.CreatedDate)
                    .Select(note => new GetStickyNoteViewModel
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
                            Branch = note.Employee.Branch,
                            FirebaseToken = note.Employee.FirebaseToken,
                            JobTitle = note.Employee.JobTitle,
                        }
                    })
                    .ToListAsync();

                return Ok(stickyNotes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

