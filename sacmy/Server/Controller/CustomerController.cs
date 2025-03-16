using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.CustomerViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using sacmy.Shared.ViewModels.Notification;
using sacmy.Shared.Core;
using sacmy.Server.Models;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        private readonly NotificationService _managerNotificationService;
        private readonly NotificationService _customerNotificationService;
        
        private readonly ILogger<NotificationService> _notificationLogger;

        public CustomerController(SafeenCompanyDbContext dbContext , IConfiguration configuration, ILogger<NotificationService> notificationLogger)
        {
            _context = dbContext ;
            
            _notificationLogger = notificationLogger ;

            _managerNotificationService = new NotificationService(
                configuration ?? throw new ArgumentNullException(nameof(configuration)),
                "SafinAhmedManagerNotificationKeys",
                _notificationLogger);

            _customerNotificationService = new NotificationService(
                configuration,
                "SafinAhmedNotificationKeys",
                _notificationLogger);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetInvoice()
        {
            var invoiceList = await _context.Customers.Select(e => new CustomerViewModel
            {
                Id = e.Id,
                Name = e.Customer1,
                PhoneNumber = e.Mob,
                Address =  e.Address,
                Branch = e.Subb,
                CostType = e.CostType,
                UserName = e.UserAcc,
                Password = e.Password,
                ConstProfit = e.PlusOne,
                ProfitPercentage = e.Nsba,
                ExtraProfitPercentage = e.OtherNsba,
                DeviceId = e.DeviceId,
                Active = e.Active,
                FirebaseToken = e.FirebaseToken
            }).OrderByDescending(e => e.Id).ToListAsync();

            return Ok(invoiceList);
        }

        [HttpPost("SendNotification/{customerName}")]
        public async Task<IActionResult> SendNotificationToCustomer(string customerName)
        {
            try
            {
                // Retrieve the customer's FirebaseToken based on customerName
                var customer = await _context.Customers
                    .Where(e => e.Customer1 == customerName && e.FirebaseToken != null)
                    .Select(e => e.FirebaseToken)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(customer))
                {
                    return NotFound($"Customer with name {customerName} not found or does not have a FirebaseToken.");
                }

                // Create notification payload
                var payload = new NotificationPayload
                {
                    Title = "تحديث على حالة ألطلبيه",
                    Body = "طلبيتك في ألمخزن الان",
                    Type = "order_status",
                    Message = customerName,
                    IsEmployeeNotification = false
                };

                var firebaseTokens = new List<string> { customer };
                await _customerNotificationService.SendNotificationAsync(payload, firebaseTokens);

                return Ok("Notification sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send notification to customer: {ex.Message}");
                return StatusCode(500, "An error occurred while sending the notification.");
            }
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult<ApiResponse<List<CustomerViewModel>>>> GetCustomers(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest(new ApiResponse<List<CustomerViewModel>>
                {
                    Success = false,
                    Message = "Invalid page number or page size",
                    Data = null
                });
            }

            var query = _context.Customers.Select(e => new CustomerViewModel
            {
                Id = e.Id,
                Name = e.Customer1,
                PhoneNumber = e.Mob,
                Address = e.Address,
                Branch = e.Subb,
                CostType = e.CostType,
                UserName = e.UserAcc,
                Password = e.Password,
                ConstProfit = e.PlusOne,
                ProfitPercentage = e.Nsba,
                ExtraProfitPercentage = e.OtherNsba,
                DeviceId = e.DeviceId,
                Active = e.Active,
                FirebaseToken = e.FirebaseToken
            });

            var totalRecords = await query.CountAsync();
            var customers = await query.OrderByDescending(e => e.Id)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();

            var response = new ApiResponse<List<CustomerViewModel>>
            {
                Success = true,
                Message = "Customers retrieved successfully",
                Data = customers
            };

            return Ok(response);
        }

        [HttpPost("BulkAddCustomerProductRelations")]
        public async Task<ActionResult<ApiResponse<object>>> BulkAddCustomerProductRelations([FromBody] List<CustomerProductRelationViewModel> relations)
        {
            if (relations == null || !relations.Any())
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid input. The list cannot be empty.",
                    Data = null
                });
            }
            try
            {
                // First get all existing customer-product relations that aren't deleted
                var existingRelations = await _context.CustomerProductRelations
                    .Where(r => !r.IsDeleted)
                    .ToListAsync();

                // Now filter in memory to find exact matches
                var existingRelationsDict = new Dictionary<string, CustomerProductRelation>();
                foreach (var relation in existingRelations)
                {
                    var key = $"{relation.CustomerId}_{relation.ProductId}";
                    existingRelationsDict[key] = relation;
                }

                var entitiesToAdd = new List<CustomerProductRelation>();
                var entitiesToUpdate = new List<CustomerProductRelation>();

                foreach (var relation in relations)
                {
                    var key = $"{relation.CustomerId}_{relation.ProductId}";

                    if (existingRelationsDict.TryGetValue(key, out var existingRelation))
                    {
                        // Update existing relation
                        existingRelation.DiscountPercentage = relation.DiscountPercentage;
                        existingRelation.IsDiscounted = relation.IsDiscounted;
                        existingRelation.RaisePercentage = relation.RaisePercentage;
                        existingRelation.IsRaised = relation.IsRaised;
                        existingRelation.IsDeleted = false;
                        // Don't update CreatedDate, but you might want to add UpdatedDate property

                        entitiesToUpdate.Add(existingRelation);
                    }
                    else
                    {
                        // Add new relation
                        var newEntity = new CustomerProductRelation
                        {
                            Id = relation.Id == Guid.Empty ? Guid.NewGuid() : relation.Id,
                            CustomerId = relation.CustomerId,
                            ProductId = relation.ProductId,
                            DiscountPercentage = relation.DiscountPercentage,
                            IsDiscounted = relation.IsDiscounted,
                            RaisePercentage = relation.RaisePercentage,
                            IsRaised = relation.IsRaised,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now
                        };

                        entitiesToAdd.Add(newEntity);
                    }
                }

                // Add new relations if any
                if (entitiesToAdd.Any())
                {
                    await _context.CustomerProductRelations.AddRangeAsync(entitiesToAdd);
                }

                // Save changes
                await _context.SaveChangesAsync();

                // Return ApiResponse with appropriate data
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = $"Operation successful. Added {entitiesToAdd.Count} new relations, updated {entitiesToUpdate.Count} existing relations.",
                    Data = new
                    {
                        NewRelations = entitiesToAdd.Select(e => e.Id).ToList(),
                        UpdatedRelations = entitiesToUpdate.Select(e => e.Id).ToList()
                    }
                });
            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"An error occurred while processing your request: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("SearchCustomers")]
        public async Task<ActionResult<ApiResponse<List<CustomerViewModel>>>> SearchCustomers(string searchQuery = "", int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest(new ApiResponse<List<CustomerViewModel>>
                {
                    Success = false,
                    Message = "Invalid page number or page size",
                    Data = null
                });
            }

            try
            {
                var query = _context.Customers.AsQueryable();

                // Apply search filter if query is provided
                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    searchQuery = searchQuery.Trim().ToLower();
                    query = query.Where(c =>
                        c.Customer1.ToLower().Contains(searchQuery) ||
                        c.Mob.Contains(searchQuery) ||
                        c.Address.ToLower().Contains(searchQuery) ||
                        c.Subb.ToLower().Contains(searchQuery)
                    );
                }

                // Count total records after filtering
                var totalRecords = await query.CountAsync();

                // Get paginated results
                var customers = await query.Select(e => new CustomerViewModel
                {
                    Id = e.Id,
                    Name = e.Customer1,
                    PhoneNumber = e.Mob,
                    Address = e.Address,
                    Branch = e.Subb,
                    CostType = e.CostType,
                    UserName = e.UserAcc,
                    Password = e.Password,
                    ConstProfit = e.PlusOne,
                    ProfitPercentage = e.Nsba,
                    ExtraProfitPercentage = e.OtherNsba,
                    DeviceId = e.DeviceId,
                    Active = e.Active,
                    FirebaseToken = e.FirebaseToken
                })
                .OrderByDescending(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

                // Prepare and return response
                var response = new ApiResponse<List<CustomerViewModel>>
                {
                    Success = true,
                    Message = customers.Any()
                        ? "Customers retrieved successfully"
                        : "No customers match your search criteria",
                    Data = customers,
                    TotalCount = totalRecords
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<CustomerViewModel>>
                {
                    Success = false,
                    Message = $"An error occurred while searching customers: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("GetCustomerProductPriceChanges/{productId}")]
        public async Task<ActionResult<ApiResponse<List<CustomerProductPriceChangeViewModel>>>> GetCustomerProductPriceChanges(Guid productId)
        {
            try
            {
                var result = await _context.CustomerProductRelations
                    .Include(cpr => cpr.Customer)
                    .Include(cpr => cpr.Product)
                    .Where(cpr => cpr.ProductId == productId)
                    .Select(cpr => new CustomerProductPriceChangeViewModel
                    {
                        CustomerName = cpr.Customer.Customer1,
                        ProductName = cpr.Product.Name,
                        ProductSku = cpr.Product.Sku,
                        ProductPatternNumber = cpr.Product.PatternNumber,
                        IsDiscounted = cpr.IsDiscounted,
                        IsRaised = cpr.IsRaised,
                        PriceChange = cpr.IsDiscounted ? $"-{cpr.DiscountPercentage}%" : cpr.IsRaised ? $"+{cpr.RaisePercentage}%" : "0%",
                        FinalPrice = cpr.IsDiscounted
                            ? (double)cpr.Product.Price - ((double)cpr.Product.Price * (double)(cpr.DiscountPercentage ?? 0) / 100.0)
                            : cpr.IsRaised
                                ? (double)cpr.Product.Price + ((double)cpr.Product.Price * (double)(cpr.RaisePercentage ?? 0) / 100.0)
                                : (double)cpr.Product.Price
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<List<CustomerProductPriceChangeViewModel>>
                {
                    Success = true,
                    Message = "Customer product price changes retrieved successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<CustomerProductPriceChangeViewModel>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving data: {ex.Message}",
                    Data = null
                });
            }
        }


    }
}
