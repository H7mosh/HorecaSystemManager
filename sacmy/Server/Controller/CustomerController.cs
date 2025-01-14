using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.CustomerViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using sacmy.Shared.ViewModels.Notification;

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
    }
}
