using sacmy.Shared.ViewModels.BrandViewModel;
using System.Net.Http.Json;

namespace sacmy.Client.Services
{
    public class BrandService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<BrandViewModel>> GetBrandsAsync()
        {
            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<BrandViewModel>>("api/Brand");
            return response ?? new List<BrandViewModel>();
        }

        public async Task<HttpResponseMessage> CreateBrandAsync(BrandViewModel model)
        {
            var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");
            var response = await client.PostAsJsonAsync("api/Brand", model);
            return response;
        }
    }
}
