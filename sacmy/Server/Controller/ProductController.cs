using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.Products;
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
        public async Task<IActionResult> GetProducts([FromQuery] ProductsForReportPaginationRequest request)
        {
            var query = _context.Products
                .Where(e => e.BrandId == Guid.Parse("63459fb9-37cd-4119-ba73-8e614e5f308b"))
                .Select(x => new GetProductsForReportViewModel
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

            var response = new ProductsPaginatedResponse<GetProductsForReportViewModel>
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

        [HttpPost("UpdateProduct")]
        public async Task<ActionResult<ApiResponse>> UpdateProduct([FromBody] UpdateProductViewModel model)
        {
            if (model.ProductId == Guid.Empty)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "ProductId is required."
                });


            // 1. Retrieve the product from the database
            var product = await _context.Products
                                    .Include(p => p.ProductImages)
                                    .FirstOrDefaultAsync(p => p.Id == model.ProductId);

            if (product == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Product not found."
                });

            // 2. Update properties only if they are not null
            //    For reference types (string?), null means do not update; an empty string means set to ""
            //    For value types (double?, int?, etc.), null means do not update; a non-null value means update.

            if (model.BrandId.HasValue)
                product.BrandId = model.BrandId.Value;

            if (string.IsNullOrWhiteSpace(model.CollectionId))
                product.CollectionId = Guid.Parse(model.CollectionId);

            if (string.IsNullOrWhiteSpace(model.CategoryId))
                product.CategoryId = Guid.Parse(model.CategoryId);

            if (model.CatalogId.HasValue)
                product.CatalogId = model.CatalogId.Value;

            if (model.Sku != null)
                product.Sku = model.Sku;

            if (model.PatternNumber != null)
                product.PatternNumber = model.PatternNumber;

            if (model.Name != null)
                product.Name = model.Name;

            if (model.Price.HasValue)
                product.Price = double.Parse(model.Price.ToString());

            if (model.Points.HasValue)
                product.Points = model.Points.Value;

            if (model.Quantity.HasValue)
                product.Quantity = model.Quantity.Value;

            if (model.InnerType != null)
                product.InnerType = model.InnerType;

            if (model.InnerTypeCount.HasValue)
                product.InnerTypeCount = model.InnerTypeCount.Value;

            if (model.OuterType != null)
                product.OuterType = model.OuterType;

            if (model.OuterTypeCount.HasValue)
                product.OuterTypeCount = model.OuterTypeCount.Value;

            if (model.Height.HasValue)
                product.Height = model.Height.Value;

            if (model.Diameter.HasValue)
                product.Diameter = model.Diameter.Value;

            if (model.Top.HasValue)
                product.Top = model.Top.Value;

            if (model.Base.HasValue)
                product.Base = model.Base.Value;

            if (model.Volume.HasValue)
                product.Volume = double.Parse(model.Volume.ToString());

            if (model.Weight.HasValue)
                product.Weight = model.Weight.Value;

            if (model.Area.HasValue)
                product.Area = model.Area.Value;

            if (model.Ean != null)
                product.Ean = model.Ean;

            // 4. Update metadata
            product.UpdateDate = DateTime.UtcNow;

            // 5. Save changes
            await _context.SaveChangesAsync();

            // 6. (Optional) Return updated product or a success message
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product updated successfully.",
                Data = product
            });
        }

        [HttpGet("GetProductById/{productId}")]
        public async Task<ActionResult<ApiResponse>> GetProductById(Guid productId)
        {
            try
            {
                // 1. Query the product with all necessary includes
                var product = await _context.Products
                    .Include(p => p.ProductImages)
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Collection)
                    .Include(p => p.Material)
                    .Include(p => p.Catalog)
                    .Where(p => p.Id == productId && !p.IsDeleted)
                    .Select(p => new
                    {
                        p.Id,
                        p.BrandId,
                        Brand = p.Brand.NameEn,
                        p.CollectionId,
                        Collection = p.Collection.NameEn,
                        p.CategoryId,
                        Category = p.Category.NameEn,
                        p.MaterialId,
                        Material = p.Material.NameEn,
                        p.CatalogId,
                        Catalog = p.Catalog.NameEn,
                        p.Sku,
                        p.PatternNumber,
                        p.Name,
                        p.Decription,
                        p.Price,
                        p.Points,
                        p.Quantity,
                        p.InnerType,
                        p.InnerTypeCount,
                        p.InnerTypeImage,
                        p.OuterType,
                        p.OuterTypeCount,
                        p.OuterTypeImages,
                        p.Height,
                        p.Diameter,
                        p.Top,
                        p.Base,
                        p.Volume,
                        p.Weight,
                        p.Area,
                        p.Upc,
                        p.Ean,
                        p.CreatedDate,
                        p.UpdateDate,
                        Images = p.ProductImages
                            .Where(pi => !pi.IsDeleted)
                            .Select(pi => new
                            {
                                pi.Id,
                                ImageLink = pi.ImageLink.Replace("https://api.safinahmedtech.com", "http://46.165.247.249"),
                                pi.CreatedDate
                            })
                            .OrderBy(pi => pi.CreatedDate)
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Product not found."
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Product retrieved successfully.",
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while retrieving the product.",
                    Data = ex.Message
                });
            }
        }

        [HttpDelete("DeleteProductImage/{imageId}")]
        public async Task<ActionResult<ApiResponse>> DeleteProductImage(Guid imageId)
        {
            try
            {
                // Find the product image
                var productImage = await _context.ProductImages
                    .FirstOrDefaultAsync(pi => pi.Id == imageId);

                if (productImage == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Product image not found."
                    });
                }

                // Get the file name from the image link
                var fileName = Path.GetFileName(productImage.ImageLink);

                // Delete the image from the server
                var imageService = new ImageService();
                var isDeleted = await imageService.RemoveImageAsync(fileName, "C:/assets/AppImage");

                if (!isDeleted)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to delete image file from server."
                    });
                }

                // Delete the record from database
                _context.ProductImages.Remove(productImage);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Product image deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while deleting the product image: {ex.Message}"
                });
            }
        }
    }

}
