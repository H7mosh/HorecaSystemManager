using Newtonsoft.Json;
using sacmy.Shared.ViewModels.Products;
using System.Net.Http.Json;
using System.Text;

namespace sacmy.Client.Services
{
    public class ProductsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.safinahmedtech.com/safenahmedapi/store";

        public ProductsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BrandResponse> GetProductsByBrandAsync(string brandId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<BrandResponse>($"{BaseUrl}/getproductsbybrand?BrandId={brandId}");
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Log the error or handle it appropriately
                throw new Exception($"Error fetching products: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                throw new Exception($"Error deserializing response: {ex.Message}", ex);
            }
        }

        public async Task<HttpResponseMessage> UpdateProductAsync(UpdateProductViewModel model)
        {
            // Option A: Use Newtonsoft.Json explicitly
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/Products/UpdateProduct", content);
            return response;
        }
    }

}
