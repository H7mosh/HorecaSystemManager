using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.InvoiceViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;

        public EmployeeController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var invoiceList = await _context.Employees.Include(e => e.Role).Select(e => new GetEmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PhoneNumber = e.PhoneNumber,
                Role = e.Role.Role,
                Branch = e.Branch,
                Brand = e.Brand,
                JobTitle = e.JobTitle,
                Image = e.Image,
                FirebaseToken = e.FirebaseToken,
                CreatedDate = e.CreatedDate,
            }).OrderBy(e => e.FirstName).ToListAsync();

            return Ok(invoiceList);
        }
    }
}
