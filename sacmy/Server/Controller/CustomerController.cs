using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.CustomerViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private SafeenCompanyDbContext _context;
        public CustomerController(SafeenCompanyDbContext context)
        {
            _context = context;
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
    }
}
