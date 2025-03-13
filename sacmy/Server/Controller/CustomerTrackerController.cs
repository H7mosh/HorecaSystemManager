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
        public CustomerTrackerController(SafeenCompanyDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> GetCostumerTotalRemain()
        {
            var customers = await _context.Customers
                .Include(c => c.Tasks)
                .ThenInclude(t => t.Type)
                .Include(c => c.Tasks)
                .ThenInclude(t => t.TaskNotes)
                .Include(c => c.Tasks)
                .ThenInclude(t => t.Status)
                .Where(c => !string.IsNullOrEmpty(c.Customer1)) // Filter out customers with null or empty Customer1
                .GroupBy(x => x.Customer1)
                .Select(g => g.FirstOrDefault())
                .ToListAsync();

            var costumerCountTRunnings = await _context.QqqCountCostumerNewTRunings.ToListAsync();

            // تحديد التاريخ الذي يسبق 45 يومًا من الآن
            var dateThreshold = DateTime.Now.AddDays(-45);

            // إيجاد العملاء الذين لديهم وصل قبض أو وصل قبض IQD خلال آخر 45 يومًا
            var customersWithRecentReceipt = costumerCountTRunnings
                .Where(q => (q.EventId.ToLower().Contains("pfc".ToLower()) || q.EventId.ToLower().Contains("pf".ToLower())) && q.Datee >= dateThreshold)
                .Select(q => q.Costumer)
                .Distinct()
                .ToHashSet();

            var deptCustomerViewModel = new List<DeptCustomerViewModel>();

            foreach (var group in costumerCountTRunnings.GroupBy(q => q.Costumer))
            {
                var customer = customers.FirstOrDefault(e => e.Customer1.ToLower() == group.Key.ToLower());

                if (customer != null)
                {
                    var lastTask = customer.Tasks?.LastOrDefault();
                    if (lastTask != null)
                    {
                        var taskType = lastTask.Type?.TypeAr;
                        var taskStatus = lastTask.Status?.StateEn;
                        var lastTaskComment = lastTask.TaskNotes?.LastOrDefault()?.Note;

                        deptCustomerViewModel.Add(new DeptCustomerViewModel
                        {
                            Id = customer.Id,
                            CustomerName = group.Key,
                            TotalTransTotalN = group.Sum(q => q.TransTotalN ?? 0),
                            HasRecentReceipt = customersWithRecentReceipt.Contains(group.Key) ? "نعم" : "لا",
                            TaskId = lastTask.Id,
                            TaskStatus = taskStatus,
                            LastComment = lastTaskComment,
                        });
                    }
                    else
                    {
                        deptCustomerViewModel.Add(new DeptCustomerViewModel
                        {
                            Id = customer.Id,
                            CustomerName = group.Key,
                            TotalTransTotalN = group.Sum(q => q.TransTotalN ?? 0),
                            HasRecentReceipt = customersWithRecentReceipt.Contains(group.Key) ? "نعم" : "لا",
                            TaskId = null,
                            TaskStatus = null,
                            LastComment = null,
                        });
                    }
                }
            }

            // Filter results where TotalTransTotalN > 200
            var result = deptCustomerViewModel.Where(e => e.TotalTransTotalN > 200).ToList();

            return Ok(result);
        }

        [HttpGet("GetHiddenCustomer")]
        public async Task<ActionResult<List<CustomerHiddenViewModel>>> GetHidderCustomer()
        {
            var thresholdDate = DateTime.Now.AddDays(-25);
            try
            {
                // Get customers first
                var customers = await _context.Customers
                                    .Include(c => c.Tasks)
                                    .ThenInclude(t => t.Type)
                                    .Include(c => c.Tasks)
                                    .ThenInclude(t => t.TaskNotes)
                                    .Include(c => c.Tasks)
                                    .ThenInclude(t => t.Status)
                                    .GroupBy(x => x.Customer1)
                                    .Select(g => g.First())
                                    .ToListAsync();

                var invoices = await _context.BuyFatoras.ToListAsync();

                // Initialize an empty dictionary for sticky notes
                Dictionary<string, List<StickyNote>> stickyNotesGrouped = new Dictionary<string, List<StickyNote>>();

                // Only try to get sticky notes if we have customers
                if (customers.Any())
                {
                    try
                    {
                        // Collect all customer IDs
                        var customerIds = customers.Select(c => c.Id.ToString()).ToList();

                        // Get sticky notes for all these customers at once
                        // Use a safer approach to query
                        var stickyNotes = await _context.StickyNotes
                            .Include(sn => sn.Employee)
                            .Where(sn => sn.TableName == "Customers")
                            .ToListAsync();

                        // Then filter in memory to avoid SQL syntax issues
                        stickyNotes = stickyNotes.Where(sn => customerIds.Contains(sn.RecordId)).ToList();

                        // Group sticky notes by customer ID
                        stickyNotesGrouped = stickyNotes
                            .GroupBy(sn => sn.RecordId)
                            .ToDictionary(g => g.Key, g => g.OrderByDescending(n => n.CreatedDate).ToList());
                    }
                    catch (Exception ex)
                    {
                        // Log the error but continue - we don't want to fail the whole request
                        // just because we couldn't get sticky notes
                        Console.WriteLine($"Error retrieving sticky notes: {ex.Message}");
                        // Continue with empty sticky notes
                    }
                }

                var customerViewModels = new List<CustomerHiddenViewModel>();

                foreach (var customer in customers)
                {
                    var lastInvoice = invoices
                        .Where(i => i.Customer == customer.Customer1)
                        .OrderByDescending(i => i.Datee)
                        .FirstOrDefault();

                    if (lastInvoice != null && lastInvoice.Datee < thresholdDate)
                    {
                        var lastTask = customer.Tasks?.LastOrDefault();

                        // Look for sticky notes for this customer
                        stickyNotesGrouped.TryGetValue(customer.Id.ToString(), out var customerNotes);

                        if (lastTask != null)
                        {
                            var taskType = lastTask.Type?.TypeAr;
                            var taskStatus = lastTask.Status?.StateEn;
                            var lastTaskComment = lastTask.TaskNotes?.LastOrDefault()?.Note;

                            customerViewModels.Add(new CustomerHiddenViewModel
                            {
                                Id = customer.Id,
                                Name = customer.Customer1,
                                Location = customer.Address,
                                Type = customer.CostType,
                                LastDate = lastInvoice.Datee,
                                TaskId = lastTask.Id,
                                TaskStatus = taskStatus,
                                LastComment = lastTaskComment,
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
                            });
                        }
                        else
                        {
                            customerViewModels.Add(new CustomerHiddenViewModel
                            {
                                Id = customer.Id,
                                Name = customer.Customer1,
                                Location = customer.Address,
                                Type = customer.CostType,
                                LastDate = lastInvoice.Datee,
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
                            });
                        }
                    }
                }

                return Ok(customerViewModels);
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

