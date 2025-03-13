using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
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

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid brand data.");
            }

            try
            {
                var brand = new Brand
                {
                    Id = Guid.NewGuid(),
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    NameKr = model.NameKr,
                    NameTr = model.NameTr,
                    Image = model.Image,
                    CreatedDate = DateTime.UtcNow
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBrands), new { id = brand.Id }, brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
