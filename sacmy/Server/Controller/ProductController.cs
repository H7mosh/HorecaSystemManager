using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.Products;
using sacmy.Shared.ViewModels.StickNoteViewModel;
using sacmy.Shared.ViewModels.TasksViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        private readonly StoreService _storeService;
        private readonly ILogger<OrdersController> _logger;

        public ProductController(SafeenCompanyDbContext context, StoreService storeService, ILogger<OrdersController> logger)
        {
            _context = context;
            _storeService = storeService;
            _logger = logger;
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetProductsByBrandId(string brandId)
        {
            try
            {
                _logger.LogInformation($"Received request for brandId: {brandId}");

                // Validate brandId
                if (string.IsNullOrEmpty(brandId))
                {
                    return BadRequest("BrandId is required.");
                }

                // Fetch the brand data (cached or otherwise)
                var brandResponse = await _storeService.GetCachedBrandData(brandId);

                if (brandResponse == null
                    || brandResponse.Data == null
                    || brandResponse.Data.Products == null
                    || !brandResponse.Data.Products.Any())
                    {
                        // Return an empty BrandResponse if no products are found
                        return Ok(new BrandResponse
                        {
                            Id = brandId,
                            Data = new BrandData
                            {
                                Products = new List<sacmy.Shared.ViewModels.Products.Product>(),
                                Categories = new List<sacmy.Shared.ViewModels.Products.Category>(),
                                Collections = new List<sacmy.Shared.ViewModels.Products.Collection>(),
                                Advertises = new List<sacmy.Shared.ViewModels.Products.Advertise>()
                            }
                        });
                    }

                // -----------------------------------------------------------------------------------
                // 1) Get all products (already done in brandResponse)
                var allProducts = brandResponse.Data.Products;

                // 2) Extract Product IDs
                var productGuids = allProducts
                        .Select(p => p.Id) 
                        .ToList();

                _logger.LogInformation($"Product GUIDs: {string.Join(", ", productGuids.Take(5))}");


                // 3) Fetch StickyNotes for these Product IDs
                var productNotes = await _context.StickyNotes
                    .Include(sn => sn.Employee)
                    .Where(sn => sn.TableName == "Products")
                    .ToListAsync();

                // Then filter in memory
                var stickyNotes = productNotes
                    .Where(sn => productGuids.Contains(sn.RecordId))
                    .ToList();

                // 4) Group the sticky notes by RecordId
                var stickyNotesGrouped = stickyNotes
                    .GroupBy(sn => sn.RecordId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // 5) Map sticky notes onto each product
                foreach (var product in allProducts)
                {
                    if (stickyNotesGrouped.TryGetValue(product.Id , out var notesForThisProduct))
                    {
                        // Convert each StickyNote to your GetStickyNoteViewModel
                        product.StickyNotes = notesForThisProduct.Select(sn => new GetStickyNoteViewModel
                        {
                            Id = sn.Id,
                            TableName = sn.TableName,
                            RecordId = sn.RecordId,
                            EmployeeId = sn.EmployeeId,
                            Note = sn.Note,
                            CreatedDate = sn.CreatedDate,

                            Employee = new GetEmployeeViewModel
                            {
                                Id = sn.Employee.Id,
                                FirstName = sn.Employee.FirstName,
                                LastName = sn.Employee.LastName,
                                PhoneNumber = sn.Employee.PhoneNumber,
                                JobTitle = sn.Employee.JobTitle,
                                Branch = sn.Employee.Branch,
                                Brand = sn.Employee.Brand,
                                Image = sn.Employee.Image,
                                FirebaseToken = sn.Employee.FirebaseToken,
                                CreatedDate = sn.Employee.CreatedDate
                            }
                        }).ToList();
                    }
                    else
                    {
                        // No sticky notes for this product
                        product.StickyNotes = new List<GetStickyNoteViewModel>();
                    }
                }
                // -----------------------------------------------------------------------------------

                // Return the full response with products now containing sticky notes
                var fullBrandResponse = new BrandResponse
                {
                    Id = brandResponse.Id,
                    BrandEn = brandResponse.BrandEn,
                    BrandAr = brandResponse.BrandAr,
                    BrandTr = brandResponse.BrandTr,
                    BrandKr = brandResponse.BrandKr,
                    Image = brandResponse.Image,
                    Data = new BrandData
                    {
                        Advertises = brandResponse.Data.Advertises,
                        Categories = brandResponse.Data.Categories,
                        Collections = brandResponse.Data.Collections,
                        Products = allProducts  // Now each product has StickyNotes
                    }
                };

                return Ok(fullBrandResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetProductsByBrandId)}: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("SearchBySku")]
        public async Task<ActionResult<ApiResponse<List<ProductDetailViewModel>>>> SearchBySku(string sku)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sku))
                {
                    return BadRequest(new ApiResponse<List<ProductDetailViewModel>>
                    {
                        Success = false,
                        Message = "SKU is required",
                        Data = new List<ProductDetailViewModel>()
                    });
                }

                // Search for products with the given SKU
                var products = await _context.Products
                    .Include(p => p.ProductImages)
                    .Where(p => p.Sku.Contains(sku) && !p.IsDeleted)
                    .Select(p => new ProductDetailViewModel
                    {
                        Id = p.Id.ToString(),
                        Sku = p.Sku,
                        PatternNumber = p.PatternNumber,
                        Name = p.Name,
                        Images = p.ProductImages
                            .Where(pi => !pi.IsDeleted)
                            .Select(pi => new ProductImageViewModel
                            {
                                Id = pi.Id.ToString(),
                                ImageLink = pi.ImageLink.Replace("https://api.safinahmedtech.com", "http://46.165.247.249")
                            })
                            .ToList()
                    })
                    .ToListAsync();

                if (!products.Any())
                {
                    return Ok(new ApiResponse<List<ProductDetailViewModel>>
                    {
                        Success = false,
                        Message = "No products found with the specified SKU",
                        Data = new List<ProductDetailViewModel>()
                    });
                }

                return Ok(new ApiResponse<List<ProductDetailViewModel>>
                {
                    Success = true,
                    Message = "Products retrieved successfully",
                    Data = products,
                    TotalCount = products.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(SearchBySku)}: {ex.Message}");
                return StatusCode(500, new ApiResponse<List<ProductDetailViewModel>>
                {
                    Success = false,
                    Message = $"An error occurred while searching for products: {ex.Message}",
                    Data = new List<ProductDetailViewModel>()
                });
            }
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

            if (model.Upc != null)
                product.Upc = model.Upc;

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

        [HttpPost("DeleteProductImage/{imageId}")]
        public async Task<ActionResult<ApiResponse>> DeleteProductImage(Guid imageId)
        {
            try
            {
                // 1. Find the product image
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

                // 2. Get the file name from the image link
                var fileName = Path.GetFileName(productImage.ImageLink);

                // 3. Delete the image from the server
                var imageService = new ImageService();
                var isDeletedFromServer = await imageService.RemoveImageAsync(fileName, "C:/assets/AppImage");

                if (!isDeletedFromServer)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to delete image file from server."
                    });
                }

                // 4. Soft delete (instead of removing it from the DB)
                productImage.IsDeleted = true;

                // 5. Mark the entity as modified and save changes
                // (If you're using EF Core tracking, setting the property is enough to track the change.
                //  But if needed, you can explicitly call Update. Either approach will work.)
                _context.ProductImages.Update(productImage);
                await _context.SaveChangesAsync();

                // 6. Return success response
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Product image deleted (soft delete) successfully."
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

        [HttpPost("DeleteBonnaImage/{imageId}")]
        public async Task<ActionResult<ApiResponse>> DeleteBonnaImage(Guid imageId)
        {
            try
            {
                // 1. Find the product image
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

                // 2. Get the file name from the image link
                var fileName = Path.GetFileName(productImage.ImageLink);

                // 3. Delete the image from the server
                var imageService = new ImageService();
                var isDeletedFromServer = await imageService.RemoveImageAsync(fileName, "C:/assets/BonnaImages");

                if (!isDeletedFromServer)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to delete image file from server."
                    });
                }

                // 4. Soft delete (instead of removing it from the DB)
                productImage.IsDeleted = true;

                // 5. Mark the entity as modified and save changes
                // (If you're using EF Core tracking, setting the property is enough to track the change.
                //  But if needed, you can explicitly call Update. Either approach will work.)
                _context.ProductImages.Update(productImage);
                await _context.SaveChangesAsync();

                // 6. Return success response
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Product image deleted (soft delete) successfully."
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

        [HttpPost("AddProductImage")]
        public async Task<ActionResult<ApiResponse>> AddProductImage([FromForm] IFormFile image, [FromForm] Guid productId)
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

                // Validate product exists and get SKU
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

                // Get file extension
                string extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                // Get existing images count for this product
                int existingImagesCount = await _context.ProductImages
                    .CountAsync(pi => pi.ProductId == productId && !pi.IsDeleted);

                // Generate filename based on SKU and existing images count
                string fileName;
                if (existingImagesCount == 0)
                {
                    fileName = $"{product.Sku}{extension}";
                }
                else
                {
                    fileName = $"{product.Sku}-{existingImagesCount + 1}{extension}";
                }

                // Save image using ImageService
                var imageService = new ImageService();
                await imageService.SaveImageAsync(image, "C:/assets/AppImage", fileName); // Pass the fileName to SaveImageAsync

                // Create image URL
                var imageUrl = $"https://api.safinahmedtech.com/assets/AppImage/{fileName}";

                // Create new product image record
                var productImage = new sacmy.Server.Models.ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    ImageLink = imageUrl,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                // Add to database
                _context.ProductImages.Add(productImage);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Image uploaded successfully.",
                    Data = new
                    {
                        Id = productImage.Id,
                        ImageLink = productImage.ImageLink,
                        CreatedDate = productImage.CreatedDate
                    }
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

        [HttpPost("AddBonnaImage")]
        public async Task<ActionResult<ApiResponse>> AddBonnaImage([FromForm] IFormFile image, [FromForm] Guid productId)
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

                // Validate product exists and get SKU
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

                // Get file extension
                string extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                // Get existing images count for this product
                int existingImagesCount = await _context.ProductImages
                    .CountAsync(pi => pi.ProductId == productId && !pi.IsDeleted);

                // Generate filename based on SKU and existing images count
                string fileName;
                if (existingImagesCount == 0)
                {
                    fileName = $"{product.Sku}{extension}";
                }
                else
                {
                    fileName = $"{product.Sku}-{existingImagesCount + 1}{extension}";
                }

                // Save image using ImageService
                var imageService = new ImageService();
                await imageService.SaveImageAsync(image, "C:/assets/BonnaImages", fileName); // Pass the fileName to SaveImageAsync

                // Create image URL
                var imageUrl = $"https://api.safinahmedtech.com/assets/BonnaImages/{fileName}";

                // Create new product image record
                var productImage = new sacmy.Server.Models.ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    ImageLink = imageUrl,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                // Add to database
                _context.ProductImages.Add(productImage);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Image uploaded successfully.",
                    Data = new
                    {
                        Id = productImage.Id,
                        ImageLink = productImage.ImageLink,
                        CreatedDate = productImage.CreatedDate
                    }
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
    }

}
