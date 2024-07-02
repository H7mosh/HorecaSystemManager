using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.UserViewModel;
using System.Data;
using static System.Net.WebRequestMethods;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private SafeenCompanyDbContext _context;
        public UserController(SafeenCompanyDbContext context)
        { 
            _context = context;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SigninPostRequestViewModel signinPostRequestViewModel)
        {
            var employee = await _context.Employees
                .Select(e => new UserViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Branch = e.Branch,
                    Brand = e.Brand,
                    Code = e.Code,
                    Image = "https://safinahmedcompany.com/assets/EmployeeImages/" + e.Image,
                    FirebaseToken = e.FirebaseToken,
                    JobTitle = e.JobTitle,
                    CreatedDate = e.CreatedDate,
                    PhoneNumber = e.PhoneNumber,
                    Role = e.Role.Role
                })
                .FirstOrDefaultAsync(e => e.PhoneNumber == signinPostRequestViewModel.PhoneNumber && e.Code == signinPostRequestViewModel.Password);

            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound("username or password are incorrect");
            }
        }


    }
}
