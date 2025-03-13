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
                // Construct the endpoint with only brandId
                var url = $"api/Product/{brandId}";

                // Create a custom HttpClient with extended timeout
                using var customClient = _httpClientFactory.CreateClient("sacmy.ServerAPI");
                customClient.Timeout = TimeSpan.FromMinutes(5); // Extend timeout to 5 minutes

                // Log the full request details
                Console.WriteLine($"[{DateTime.Now}] Starting request");
                Console.WriteLine($"Base Address: {customClient.BaseAddress}");
                Console.WriteLine($"Relative URL: {url}");
                Console.WriteLine($"Full URL: {new Uri(customClient.BaseAddress, url)}");
                Console.WriteLine($"Timeout set to: {customClient.Timeout}");

                // Make the request with cancellation token that respects the timeout
                Console.WriteLine($"[{DateTime.Now}] Sending request...");
                var response = await customClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                Console.WriteLine($"[{DateTime.Now}] Response headers received");

                // Read content as stream to avoid memory issues with large responses
                using var contentStream = await response.Content.ReadAsStreamAsync();
                Console.WriteLine($"[{DateTime.Now}] Response stream opened");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Response Status: {response.StatusCode}");

                    // Use System.Text.Json directly with stream for better performance
                    var options = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    };

                    Console.WriteLine($"[{DateTime.Now}] Deserializing response...");
                    var content = await System.Text.Json.JsonSerializer.DeserializeAsync<BrandResponse>(contentStream, options);
                    Console.WriteLine($"[{DateTime.Now}] Deserialization complete");

                    if (content != null)
                    {
                        var productCount = content.Data?.Products?.Count ?? 0;
                        Console.WriteLine($"Received {productCount} products");
                        return content;
                    }
                    else
                    {
                        Console.WriteLine("Warning: Deserialized content is null");
                        return CreateEmptyBrandResponse(brandId);
                    }
                }
                else
                {
                    // Read error content differently to avoid issues
                    using var reader = new StreamReader(contentStream);
                    var errorContent = await reader.ReadToEndAsync();

                    // Log any error text
                    Console.WriteLine($"Error Response Status: {response.StatusCode}");
                    Console.WriteLine($"Error Response: {errorContent}");

                    return CreateEmptyBrandResponse(brandId);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Request timed out: {ex.Message}");
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

        // Helper method to create empty brand response
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
                    Console.WriteLine($"Error Status: {response.StatusCode}");
                    Console.WriteLine($"Error Content: {errorContent}");
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

                // ✅ Make POST request
                var response = await client.PostAsJsonAsync("api/lowstock/monitor", model);

                // ✅ Debug Logging
                Console.WriteLine($"Request URL: {client.BaseAddress}api/lowstock/monitor");
                Console.WriteLine($"Request payload: {JsonConvert.SerializeObject(model)}");

                // ✅ Deserialize response directly as `ApiResponse`
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

                // Create multipart form content
                using var content = new MultipartFormDataContent();

                // Add the image file
                var imageContent = new StreamContent(imageStream);
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // Adjust based on file type
                content.Add(imageContent, "image", fileName);

                // Add the product ID
                content.Add(new StringContent(productId), "productId");
                HttpResponseMessage response;

                // Send the request
                if (brand == "boona")
                {
                    response = await client.PostAsync("api/Product/AddProductImage", content);
                }
                else
                {
                    response = await client.PostAsync("api/Product/AddBonnaImage", content);
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
                var response = await client.DeleteAsync($"api/Product/DeleteProductImage/{imageId}");

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
                // Construct the URL using the base URL constant
                var url = $"{BaseUrl}/resetcache";
                Console.WriteLine($"Calling URL: {url}");

                // Send POST request without body
                var response = await _httpClient.PostAsync(url, null);

                // Read the response content
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Raw Response: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API returned {response.StatusCode}: {content}");
                }

                return content; // This should contain "Cache reset and refreshed successfully."
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

    }
}
