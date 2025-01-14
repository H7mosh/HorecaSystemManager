using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.ViewModels.ProductsViewModel;
using sacmy.Shared.ViewModels.TasksViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        public ProductController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductsPaginationRequest request)
        {
            var query = _context.Products
                .Where(e => e.BrandId == Guid.Parse("63459fb9-37cd-4119-ba73-8e614e5f308b"))
                .Select(x => new GetProductsViewModel
                {
                    Id = x.Id,
                    BrandName = x.Brand.NameEn,
                    CollectionName = x.Collection.NameEn,
                    Sku = x.Sku,
                    PatternNumber = x.PatternNumber,
                    BoxCount = x.OuterTypeCount,
                    PieceCount = x.InnerTypeCount,
                    Price = x.Price.ToString(),
                    Image = x.ProductImages.FirstOrDefault().ImageLink.Replace("https://api.safinahmedtech.com", "http://46.165.247.249"),
                });

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            if (items is null || !items.Any())
            {
                return Ok("There's No Items Yet!");
            }

            var response = new ProductsPaginatedResponse<GetProductsViewModel>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber,
                HasNext = request.PageNumber < totalPages,
                HasPrevious = request.PageNumber > 1
            };

            return Ok(response);
        }
    }

}
