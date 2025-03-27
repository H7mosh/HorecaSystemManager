using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.BrandViewModel;
using sacmy.Shared.ViewModels.LowStockViewModels;
using sacmy.Shared.ViewModels.Products;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace sacmy.Client.Services
{
    public class ProductsService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        private const string BaseUrl = "https://api.safinahmedtech.com/safenahmedapi/store";

        public ProductsService(HttpClient httpClient , IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BrandResponse> GetProductsByBrandAsync(string brandId)
        {
            try
            {
                var url = $"api/Product/{brandId}";

                using var customClient = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                customClient.Timeout = TimeSpan.FromMinutes(5); 

                var response = await customClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                using var contentStream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    };

                    var content = await System.Text.Json.JsonSerializer.DeserializeAsync<BrandResponse>(contentStream, options);

                    if (content != null)
                    {
                        var productCount = content.Data?.Products?.Count ?? 0;
                        return content;
                    }
                    else
                    {
                        return CreateEmptyBrandResponse(brandId);
                    }
                }

                else
                {
                    using var reader = new StreamReader(contentStream);

                    var errorContent = await reader.ReadToEndAsync();

                    return CreateEmptyBrandResponse(brandId);
                }
            }
            catch (TaskCanceledException ex)
            {
                return CreateEmptyBrandResponse(brandId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Exception type: {ex.GetType().Name}");
                Console.WriteLine($"Exception message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<ApiResponse<List<ProductDetailViewModel>>> SearchProductsBySkuAsync(string sku)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sku))
                {
                    return new ApiResponse<List<ProductDetailViewModel>>
                    {
                        Success = false,
                        Message = "SKU is required",
                        Data = new List<ProductDetailViewModel>()
                    };
                }

                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<ProductDetailViewModel>>>($"api/Product/SearchBySku?sku={sku}");

                if (response == null)
                {
                    return new ApiResponse<List<ProductDetailViewModel>>
                    {
                        Success = false,
                        Message = "Failed to get response from server",
                        Data = new List<ProductDetailViewModel>()
                    };
                }

                return response;
            }
            catch (Exception ex)
            {

                return new ApiResponse<List<ProductDetailViewModel>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = new List<ProductDetailViewModel>()
                };
            }
        }

        private BrandResponse CreateEmptyBrandResponse(string brandId)
        {
            return new BrandResponse
            {
                Id = brandId,
                Data = new BrandData
                {
                    Products = new List<Product>(),
                    Categories = new List<Category>(),
                    Collections = new List<Collection>(),
                    Advertises = new List<Advertise>()
                }
            };
        }

        public async Task<ApiResponse> UpdateProductAsync(UpdateProductViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                var response = await client.PostAsJsonAsync("api/Product/UpdateProduct", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                }

                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"API request failed with status {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Request failed: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<ProductDetailViewModel>> GetProductByIdAsync(string productId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                var response = await client.GetAsync($"api/Product/GetProductById/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse<ProductDetailViewModel>>(responseContent);
                }

                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<ProductDetailViewModel>
                    {
                        Success = false,
                        Message = $"API request failed with status {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDetailViewModel>
                {
                    Success = false,
                    Message = $"Request failed: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> MonitorProductAsync(MonitorProductViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                var response = await client.PostAsJsonAsync("api/lowstock/monitor", model);

                var responseContent = await response.Content.ReadFromJsonAsync<ApiResponse>();

                return responseContent ?? new ApiResponse { Success = false, Message = "Empty response from API" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Request failed: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> AddProductImageAsync(string productId, Stream imageStream, string fileName, string brand)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                using var content = new MultipartFormDataContent();

                var imageContent = new StreamContent(imageStream);

                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); 

                content.Add(imageContent, "image", fileName);

                content.Add(new StringContent(productId), "productId");

                HttpResponseMessage response;

                if (brand == "boona")
                {
                    response = await client.PostAsync("api/Product/AddBonnaImage", content);
                }

                else
                {
                    response = await client.PostAsync("api/Product/AddProductImage", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                }

                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Status: {response.StatusCode}");
                    Console.WriteLine($"Error Content: {errorContent}");
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"API request failed with status {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Request failed: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> DeleteProductImageAsync(string imageId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                // Create an empty content since PostAsync requires a content parameter
                var emptyContent = new StringContent("", Encoding.UTF8, "application/json");

                // Send the request with the empty content
                var response = await client.PostAsync($"api/Product/DeleteProductImage/{imageId}", emptyContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Status: {response.StatusCode}");
                    Console.WriteLine($"Error Content: {errorContent}");
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"API request failed with status {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Request failed: {ex.Message}"
                };
            }
        }

        public async Task<string> ResetCacheAsync()
        {
            try
            {
                var url = $"{BaseUrl}/resetcache";

                var response = await _httpClient.PostAsync(url, null);

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API returned {response.StatusCode}: {content}");
                }

                return content; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}
