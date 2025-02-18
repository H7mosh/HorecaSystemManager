using Microsoft.EntityFrameworkCore;
using sacmy.Client.Services;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.BrandViewModel;
using sacmy.Shared.ViewModels.Products;

namespace sacmy.Server.Service
{
    public class StoreService
    {
        private readonly SafeenCompanyDbContext _context;
        private readonly RedisCacheService _cacheService;
        private readonly ILogger<StoreService> _logger;

        public StoreService(SafeenCompanyDbContext context, RedisCacheService cacheService, ILogger<StoreService> logger)
        {
            _context = context;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task FetchAndCacheKPDataAsync()
        {
            try
            {
                var kpStoreData = await _context.KpStores
                    .Where(kp => kp.BrandId != null)
                    .Select(kp => new
                    {
                        kp.Id,
                        kp.BrandId,
                        kp.BrandEn,
                        kp.BrandAr,
                        kp.BrandTr,
                        kp.BrandKr,
                        kp.BrandImage,
                        kp.CollectionId,
                        kp.CollectionEn,
                        kp.CollectionAr,
                        kp.CollectionTr,
                        kp.CollectionKr,
                        kp.CollectionImage,
                        kp.CategoryId,
                        kp.CategoryEn,
                        kp.CategoryAr,
                        kp.CategoryTr,
                        kp.CategoryKr,
                        kp.CategoryImage,
                        kp.MaterialId,
                        kp.CatalogId,
                        kp.Sku,
                        kp.PatternNumber,
                        kp.Name,
                        kp.Decription,
                        kp.Price,
                        kp.Points,
                        kp.QttyPurch,
                        kp.QttySales,
                        kp.QttyReturn,
                        kp.Ean,
                        kp.Upc,
                        kp.OuterType,
                        kp.OuterTypeCount,
                        kp.InnerType,
                        kp.InnerTypeCount,
                        kp.Height,
                        kp.Diameter,
                        kp.Top,
                        kp.Base,
                        kp.Volume,
                        kp.Weight,
                        kp.Area,
                        kp.CreatedDate,
                        kp.ImageLink,
                    }).AsNoTracking().ToListAsync();

                if (!kpStoreData.Any())
                {
                    _logger.LogInformation("No items found in the database.");
                    return;
                }

                // Fetch Advertise data
                var advertiseData = await _context.Advertises
                    .Select(ad => new AdvertiseViewModel
                    {
                        Id = ad.Id,
                        Image = ad.Image,
                        ProductId = ad.ProductId,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                // Group by Brand and create view models
                var brands = kpStoreData
                            .GroupBy(kp => new
                            {
                                kp.BrandId,
                                kp.BrandEn,
                                kp.BrandAr,
                                kp.BrandTr,
                                kp.BrandKr,
                                kp.BrandImage
                            })
                            .Select(brandGroup => new BrandResponse
                            {
                                Id = brandGroup.Key.BrandId.ToString(),
                                BrandEn = brandGroup.Key.BrandEn,
                                BrandAr = brandGroup.Key.BrandAr,
                                BrandTr = brandGroup.Key.BrandTr,
                                BrandKr = brandGroup.Key.BrandKr,
                                Image = brandGroup.Key.BrandImage,
                                Data = new BrandData
                                {
                                    // Use Distinct() to ensure unique categories
                                    Categories = brandGroup
                                        .Where(kp => kp.CategoryId.HasValue)
                                        .Select(kp => new Category
                                        {
                                            Id = kp.CategoryId.Value.ToString(),
                                            NameEn = kp.CategoryEn,
                                            NameAr = kp.CategoryAr,
                                            NameTr = kp.CategoryTr,
                                            NameKr = kp.CategoryKr,
                                            Image = kp.CategoryImage
                                        })
                                        .DistinctBy(c => c.Id) // Remove duplicate categories by Id
                                        .ToList(),

                                    // Use Distinct() to ensure unique collections
                                    Collections = brandGroup
                                        .Where(kp => kp.CollectionId.HasValue)
                                        .Select(kp => new Collection
                                        {
                                            Id = kp.CollectionId.Value.ToString(),
                                            NameEn = kp.CollectionEn,
                                            NameAr = kp.CollectionAr,
                                            NameTr = kp.CollectionTr,
                                            NameKr = kp.CollectionKr
                                        })
                                        .DistinctBy(c => c.Id) // Remove duplicate collections by Id
                                        .ToList(),



                                    Products = brandGroup.Select(kp => new Product
                                    {
                                        Id = kp.Id.ToString(),
                                        BrandId = kp.BrandId.ToString(),
                                        CategoryId = kp.CategoryId.ToString() ?? string.Empty,
                                        SeriesOrCollectionId = kp.CollectionId.ToString() ?? string.Empty,
                                        CatalogId = kp.CatalogId.ToString() ?? string.Empty,
                                        MaterialId = kp.MaterialId.ToString(),
                                        Sku = kp.Sku,
                                        PatternNumber = kp.PatternNumber,
                                        Ean = kp.Ean,
                                        Upc = kp.Upc,
                                        Name = kp.Name,
                                        Description = kp.Decription,
                                        Price = (decimal?)kp.Price,
                                        Points = kp.Points ?? 0,
                                        Quantity = int.Parse(((kp.QttyPurch ?? 0) - (kp.QttySales ?? 0) + (kp.QttyReturn ?? 0)).ToString()),
                                        BoxCount = kp.OuterTypeCount,
                                        BoxCountType = kp.OuterType,
                                        PieceCount = kp.InnerTypeCount,
                                        PieceCountType = kp.InnerType,
                                        Area = (decimal?)kp.Area,
                                        Base = (decimal?)kp.Base,
                                        Diameter = (decimal?)kp.Diameter,
                                        Height = (decimal?)kp.Height,
                                        Top = (decimal?)kp.Top,
                                        Weight = (decimal?)kp.Weight,
                                        Volume = (decimal?)kp.Volume,
                                        CreatedDate = kp.CreatedDate,
                                        IsNew = (DateTime.Now - kp.CreatedDate).TotalDays < 25,
                                        Image = kp.ImageLink,
                                        IsDiscounted = false,
                                        DiscountPercentage = 0,
                                        IsRaised = false,
                                        RaisedPercentage = 0
                                    }).OrderByDescending(e => e.Quantity).ToList(),


                                }
                            }).ToList();


                // Cache the result including Advertise data
                await _cacheService.SetCacheDataAsync("KPData_All", brands, null); // Set no expiration

                _logger.LogInformation("KP data and Advertise cached successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching and caching KP data.");
            }
        }

        public async Task<BrandResponse> GetCachedBrandData(string brandId)
        {
            try
            {
                // Fetch cached data for all brands
                var cachedData = await _cacheService.GetCacheDataAsync<List<BrandResponse>>("KPData_All");

                if (cachedData == null || !cachedData.Any())
                {
                    // Cache is empty, fetch from the database and refresh the cache
                    await FetchAndCacheKPDataAsync();
                    cachedData = await _cacheService.GetCacheDataAsync<List<BrandResponse>>("KPData_All");
                }

                // Find the data for the requested brandId
                var brandData = cachedData.FirstOrDefault(brand => Guid.Parse(brand.Id) == Guid.Parse(brandId));
                if (brandData == null)
                {
                    _logger.LogInformation("No data found for BrandId: {brandId}", brandId);
                    return null;
                }

                _logger.LogInformation("Data fetched from cache for BrandId: {brandId}", brandId);

                return brandData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data for BrandId: {brandId}", brandId);
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByBrandAsync(string brandId)
        {
            try
            {
                // Convert brandId to Guid
                var brandGuid = Guid.Parse(brandId);

                // Fetch cached data for all brands
                var cachedData = await _cacheService.GetCacheDataAsync<List<BrandResponse>>("KPData_All");

                if (cachedData == null || !cachedData.Any())
                {
                    // Cache is empty, fetch from the database and refresh the cache
                    await FetchAndCacheKPDataAsync();
                    cachedData = await _cacheService.GetCacheDataAsync<List<BrandResponse>>("KPData_All");
                }

                // Find the data for the requested brandId
                var brandData = cachedData.FirstOrDefault(brand => brand.Id.ToString() == brandGuid.ToString());
                if (brandData == null)
                {
                    _logger.LogInformation("No data found for BrandId: {brandId}", brandId);
                    return null;
                }

                // Return the list of products
                return brandData.Data.Products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products for BrandId: {brandId}", brandId);
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<Guid> itemIds)
        {
            try
            {
                // Fetch cached data for all brands
                var cachedData = await _cacheService.GetCacheDataAsync<List<BrandResponse>>("KPData_All");

                if (cachedData == null || !cachedData.Any())
                {
                    // Cache is empty, fetch from the database and refresh the cache
                    await FetchAndCacheKPDataAsync();
                    cachedData = await _cacheService.GetCacheDataAsync<List<BrandResponse>>("KPData_All");
                }

                // Gather all products from all brands
                var allProducts = cachedData.SelectMany(brand => brand.Data.Products).ToList();

                // Filter products by itemIds
                var products = allProducts.Where(p => itemIds.Contains(Guid.Parse(p.Id))).ToList();

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products by itemIds");
                throw;
            }
        }

        public async Task ResetCacheAsync()
        {
            try
            {
                // Remove the existing cache entry for KP data
                await _cacheService.RemoveCacheDataAsync("KPData_All");

                // Re-fetch and cache KP data
                await FetchAndCacheKPDataAsync();

                _logger.LogInformation("Cache has been reset and refreshed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resetting the cache.");
                throw;
            }
        }

    }
}
