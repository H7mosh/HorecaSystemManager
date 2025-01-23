using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.BrandViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;

        public BrandController(SafeenCompanyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetBrands() {
            try
            {
                var brands = await _context.Brands.Select(
                    e => new BrandViewModel
                    {
                        Id = e.Id,
                        NameEn = e.NameEn,
                        NameAr = e.NameAr,
                        NameKr = e.NameKr,
                        NameTr = e.NameTr,
                        Image = e.Image,
                        CreatedDate = e.CreatedDate
                    }
                ).ToListAsync();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
