using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.AdvertiseVewModels;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertiseController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;

        public AdvertiseController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetAdvertiseViewModel>>>> GetAdvertises()
        {
            var advertises = await _context.Advertises
                .Include(a => a.Product)
                .Select(a => new GetAdvertiseViewModel
                {
                    Id = a.Id,
                    ProductId = a.ProductId,
                    Sku = a.Product.Sku,
                    PatternCode = a.Product.PatternNumber,
                    Image = a.Image,
                })
                .ToListAsync();

            return Ok(new ApiResponse<IEnumerable<GetAdvertiseViewModel>>
            {
                Success = true,
                Message = "Advertisements retrieved successfully",
                Data = advertises,
                TotalCount = advertises.Count
            });
        }

        // GET: api/Advertise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetAdvertiseViewModel>>> GetAdvertise(Guid id)
        {
            var advertise = await _context.Advertises
                .Include(a => a.Product)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (advertise == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Advertisement not found"
                });
            }

            var viewModel = new GetAdvertiseViewModel
            {
                Id = advertise.Id,
                ProductId = advertise.ProductId,
                Sku = advertise.Product.Sku,
                PatternCode = advertise.Product.PatternNumber,
                Image = advertise.Image,
            };

            return Ok(new ApiResponse<GetAdvertiseViewModel>
            {
                Success = true,
                Message = "Advertisement retrieved successfully",
                Data = viewModel
            });
        }

        // POST: api/Advertise
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GetAdvertiseViewModel>>> CreateAdvertise(CreateAdvertiseViewModel request)
        {
            // Validate if the product exists
            var productExists = await _context.Products.AnyAsync(p => p.Id == request.ProductId);
            if (!productExists)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "The specified product does not exist."
                });
            }

            var advertise = new Advertise
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Image = request.Image
            };

            _context.Advertises.Add(advertise);
            await _context.SaveChangesAsync();

            var viewModel = new GetAdvertiseViewModel
            {
                Id = advertise.Id,
                ProductId = advertise.ProductId,
                Image = advertise.Image
            };

            return Ok(new ApiResponse<GetAdvertiseViewModel>
            {
                Success = true,
                Message = "Advertisement created successfully",
                Data = viewModel
            });
        }

        // POST: api/Advertise/Update/5
        [HttpPost("Update/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateAdvertise(Guid id, CreateAdvertiseViewModel request)
        {
            var advertise = await _context.Advertises.FindAsync(id);
            if (advertise == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Advertisement not found"
                });
            }

            // Validate if the product exists
            var productExists = await _context.Products.AnyAsync(p => p.Id == request.ProductId);
            if (!productExists)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "The specified product does not exist."
                });
            }

            advertise.ProductId = request.ProductId;
            advertise.Image = request.Image;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertiseExists(id))
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Advertisement not found"
                    });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Advertisement updated successfully"
            });
        }

        // POST: api/Advertise/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAdvertise(Guid id)
        {
            var advertise = await _context.Advertises.FindAsync(id);
            if (advertise == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Advertisement not found"
                });
            }

            _context.Advertises.Remove(advertise);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Advertisement deleted successfully"
            });
        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult<ApiResponse>> UploadImage([FromForm] IFormFile image, [FromForm] Guid productId)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "No image file provided."
                    });
                }

                // Validate product exists
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
                if (product == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Product not found."
                    });
                }

                // Use the FileService to upload the image
                var fileService = new FileService();
                string targetPath = @"C:\assets\AdvertiseImages";
                string fileName = await fileService.UploadFileAsync(image, $"Adv-{product.Sku}", targetPath);

                // Create the image URL
                var imageUrl = $"https://api.safinahmedtech.com/assets/AdvertiseImages/{fileName}";

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Image uploaded successfully",
                    Data = imageUrl
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while uploading the image: {ex.Message}"
                });
            }
        }

        private bool AdvertiseExists(Guid id)
        {
            return _context.Advertises.Any(e => e.Id == id);
        }
    }
}
