using sacmy.Shared.ViewModels.DashboardViewModel;
using sacmy.Shared.ViewModels.InvoiceViewModel;
using System.Net.Http.Json;

namespace sacmy.Client.Services
{
    public class DashboardService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GetBranchSales>> GetBranchSalesAsync(DateTime? startDate, DateTime? endDate, string selectedBranch)
        {
            var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");
            string branchSalesUrl = $"api/dashboard/GetBranchSales?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            if (startDate.HasValue && endDate.HasValue)
            {
                branchSalesUrl = $"api/dashboard/GetBranchSales?startDate={startDate.Value:yyyy-MM-dd}&endDate={endDate.Value:yyyy-MM-dd}";
            }

            if (selectedBranch != "All")
            {
                branchSalesUrl += $"&branch={selectedBranch}";
            }

            var branchSales = await client.GetFromJsonAsync<List<GetBranchSales>>(branchSalesUrl);
            return branchSales;
        }
    }

}
