using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.CustomerViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private SafeenCompanyDbContext _context;
        private readonly NotificationService _notificationService;
        public CustomerController(SafeenCompanyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _notificationService = new NotificationService(configuration, "SafinAhmedNotificationKeys"); // Use customer notifications key
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
            // Retrieve the customer's FirebaseToken based on customerId
            var customer = await _context.Customers
                .Where(e => e.Customer1 == customerName && e.FirebaseToken != null)
                .Select(e => e.FirebaseToken)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(customer))
            {
                return NotFound($"Customer with ID {customerName} not found or does not have a FirebaseToken.");
            }

            // Call the notification service to send the notification
            var title = "تحديث على حالة ألطلبيه";
            var body = "طلبيتك في ألمخزن الان";
            var firebaseTokens = new List<string> { customer };

            await _notificationService.SendNotificationAsync(title, body, firebaseTokens, false);

            return Ok("Notification sent successfully.");
        }
    }
}
