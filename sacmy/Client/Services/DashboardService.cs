using BlazorBootstrap;
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

        public async Task LoadInvoiceCounts(DateTime? startDate, DateTime? endDate, string selectedBranch, ChartData chartData, PieChartOptions pieChartOptions, List<string> backgroundColors, PieChart pieChart, Func<Task> stateHasChanged)
        {
            var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");

            string invoiceUrl = "api/invoice/InvoiceCounts";
            string branchSalesUrl = $"api/dashboard/GetBranchSales?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            if (startDate.HasValue && endDate.HasValue)
            {
                invoiceUrl += $"?startDate={startDate.Value:yyyy-MM-dd}&endDate={endDate.Value:yyyy-MM-dd}";
                branchSalesUrl = $"api/dashboard/GetBranchSales?startDate={startDate.Value:yyyy-MM-dd}&endDate={endDate.Value:yyyy-MM-dd}";

                if (selectedBranch != "All")
                {
                    invoiceUrl += $"&branch={selectedBranch}";
                }
            }

            var invoiceCounts = await client.GetFromJsonAsync<List<InvoiceTypeViewModel>>(invoiceUrl);
            chartData.Labels.Clear();
            chartData.Datasets.Clear();
            var data = new List<double>();
            var colors = new List<string>();

            foreach (var invoiceCount in invoiceCounts)
            {
                chartData.Labels.Add(invoiceCount.Type);
                data.Add(invoiceCount.TotalAmount);
            }

            colors.AddRange(backgroundColors.Take(data.Count));

            chartData.Datasets.Add(new PieChartDataset
            {
                Data = data,
                BackgroundColor = colors
            });

            pieChartOptions = new PieChartOptions
            {
                Responsive = true,
                Plugins = new PieChartPlugins
                {
                    Legend = new ChartPluginsLegend
                    {
                        Position = "right"
                    }
                }
            };

            await pieChart.UpdateAsync(chartData, pieChartOptions);

            await stateHasChanged(); // Update the UI
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
