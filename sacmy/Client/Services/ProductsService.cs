using Newtonsoft.Json;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.Products;
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
                // Log the full URL being called
                var url = $"{BaseUrl}/getproductsbybrand?BrandId={brandId}";
                Console.WriteLine($"Calling URL: {url}");

                var response = await _httpClient.GetAsync(url);

                // Log the response status code
                Console.WriteLine($"Response Status: {response.StatusCode}");

                // Read the raw response content
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw Response: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API returned {response.StatusCode}: {content}");
                }

                return JsonConvert.DeserializeObject<BrandResponse>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
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

    }

}
