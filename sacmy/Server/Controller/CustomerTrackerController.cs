using DocumentFormat.OpenXml.Office2019.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Client.Pages.Invoice;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Shared.ViewModel.HorecaViewModel;
using sacmy.Shared.ViewModels.CustomerTracker;
using sacmy.Shared.ViewModels.TrackViewModel;
using System.Linq;
using static System.Net.WebRequestMethods;

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
                .Include(c => c.Tracks)
                    .ThenInclude(t => t.Type)
                .Include(c => c.Tracks)
                    .ThenInclude(t => t.TrackComments)
                        .ThenInclude(tc => tc.TrackCommentStates)
                            .ThenInclude(tcs => tcs.State)
                .Where(c => !string.IsNullOrEmpty(c.Customer1)) // Filter out customers with null or empty Customer1
                .GroupBy(x => x.Customer1)
                .Select(g => g.FirstOrDefault())
                .ToListAsync();

            var costumerCountTRunnings = await _context.QqqCountCostumerNewTRunings.ToListAsync();

            // تحديد التاريخ الذي يسبق 45 يومًا من الآن
            var dateThreshold = DateTime.Now.AddDays(-45);

            // إيجاد العملاء الذين لديهم وصل قبض أو وصل قبض IQD خلال آخر 45 يومًا
            var customersWithRecentReceipt = costumerCountTRunnings
                .Where(q => (q.EventId.ToLower().Contains("Bu".ToLower())) && q.Datee >= dateThreshold)
                .Select(q => q.Costumer)
                .Distinct()
                .ToHashSet();

            var result = costumerCountTRunnings
                .GroupBy(q => q.Costumer)
                .Select(g =>
                {
                    var customer = customers.FirstOrDefault(e => e.Customer1.ToLower() == g.Key.ToLower());

                    if (customer != null)
                    {
                        return new DeptCustomerViewModel
                        {
                            Id = customer.Id,
                            CustomerName = g.Key,
                            TotalTransTotalN = g.Sum(q => q.TransTotalN ?? 0),
                            HasRecentReceipt = customersWithRecentReceipt.Contains(g.Key) ? "نعم" : "لا"
                        };
                    }

                    return null; // Handle case where customer is null
                })
                .Where(e => e != null && e.TotalTransTotalN > 200) // Filter out null results
                .ToList();

            return Ok(result);
        }


        [HttpGet("GetHiddenCustomer")]
        public async Task<ActionResult<List<CustomerHiddenViewModel>>> GetHidderCustomer()
        {
            var thresholdDate = DateTime.Now.AddDays(-25);

            try
            {
                var customers = await _context.Customers
                                    .Include(c => c.Tracks)
                                    .ThenInclude(t => t.Type)
                                    .Include(c => c.Tracks)
                                    .ThenInclude(t => t.TrackComments)
                                    .ThenInclude(tc => tc.TrackCommentStates)
                                    .ThenInclude(st => st.State)
                                    .GroupBy(x => x.Customer1)
                                    .Select(g => g.First())
                                    .ToListAsync();
                
                var invoices = await _context.BuyFatoras.ToListAsync();

                var customerViewModels = new List<CustomerHiddenViewModel>();

                foreach (var customer in customers)
                {
                    var lastInvoice = invoices
                        .Where(i => i.Customer == customer.Customer1)
                        .OrderByDescending(i => i.Datee)
                        .FirstOrDefault();

                    if (lastInvoice != null && lastInvoice.Datee < thresholdDate)
                    {
                        var lastTrack = customer.Tracks.LastOrDefault();
                        if (lastTrack != null)
                        {
                            var trackType = lastTrack?.Type?.TypeAr;
                            var lastTrackComment = lastTrack.TrackComments?.LastOrDefault();
                            var lastTrackCommentState = lastTrackComment?.TrackCommentStates?.LastOrDefault();
                            var state = lastTrackCommentState?.State?.StateAr;

                            customerViewModels.Add(new CustomerHiddenViewModel
                            {
                                Id = customer.Id,
                                Name = customer.Customer1,
                                Location = customer.Address,
                                Type = customer.CostType,
                                LastDate = lastInvoice.Datee,
                                TrackId = lastTrack.Id,
                                TrackType = trackType ?? null,
                                State = state ?? null,
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
                                TrackId = null,
                                TrackType = null,
                                State = null,
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


    }
}

