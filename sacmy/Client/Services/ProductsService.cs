using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.BrandViewModel;
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

                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

                // Log the full request details
                Console.WriteLine($"Base Address: {client.BaseAddress}");
                Console.WriteLine($"Relative URL: {url}");
                Console.WriteLine($"Full URL: {new Uri(client.BaseAddress, url)}");

                // Make the request
                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Log the raw response
                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Raw Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    // If successful, deserialize the JSON into BrandResponse
                    var content = await response.Content.ReadFromJsonAsync<BrandResponse>();
                    return content;
                }
                else
                {
                    // Log any error text
                    Console.WriteLine($"Error Response: {responseContent}");

                    // Return an "empty" BrandResponse to avoid null exceptions in the UI
                    return new BrandResponse
                    {
                        Data = new BrandData
                        {
                            Products = new List<Product>(),
                            Categories = new List<Category>(),
                            Collections = new List<Collection>(),
                            Advertises = new List<Advertise>()
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception type: {ex.GetType().Name}");
                Console.WriteLine($"Exception message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
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

        public async Task<ApiResponse> AddProductImageAsync(string productId, Stream imageStream, string fileName)
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

                // Send the request
                var response = await client.PostAsync("api/Product/AddProductImage", content);

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
