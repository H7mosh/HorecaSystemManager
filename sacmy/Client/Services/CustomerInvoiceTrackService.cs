using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.CustomerTracker;
using sacmy.Shared.ViewModels.InvoiceViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace sacmy.Client.Services
{
    public class CustomerInvoiceTrackService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerInvoiceTrackService> _logger;

        public CustomerInvoiceTrackService(HttpClient httpClient, ILogger<CustomerInvoiceTrackService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(List<CustomerHiddenViewModel> customers, PaginationMetadata metadata)> GetHiddenCustomersAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = null)
        {
            try
            {
                var apiUrl = $"api/CustomerTracker/GetHiddenCustomer?pageNumber={pageNumber}&pageSize={pageSize}";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    apiUrl += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
                }

                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API returned {response.StatusCode} when fetching hidden customers");
                    return (new List<CustomerHiddenViewModel>(), new PaginationMetadata());
                }

                var metadata = new PaginationMetadata();
                if (response.Headers.TryGetValues("X-Pagination", out var paginationHeaderValues))
                {
                    metadata = JsonSerializer.Deserialize<PaginationMetadata>(
                        paginationHeaderValues.FirstOrDefault());
                }

                var customers = await response.Content.ReadFromJsonAsync<List<CustomerHiddenViewModel>>();
                return (customers, metadata);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching hidden customers");
                return (new List<CustomerHiddenViewModel>(), new PaginationMetadata());
            }
        }

        // Overload that accepts the date filter parameter
        public async Task<(List<CustomerHiddenViewModel> customers, PaginationMetadata metadata)> GetHiddenCustomersAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = null,
            string dateFilter = "default")
        {
            try
            {
                var apiUrl = $"api/CustomerTracker/GetHiddenCustomer?pageNumber={pageNumber}&pageSize={pageSize}";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    apiUrl += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
                }

                if (!string.IsNullOrWhiteSpace(dateFilter) && dateFilter != "default")
                {
                    apiUrl += $"&dateFilter={Uri.EscapeDataString(dateFilter)}";
                }

                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API returned {response.StatusCode} when fetching hidden customers");
                    return (new List<CustomerHiddenViewModel>(), new PaginationMetadata());
                }

                var metadata = new PaginationMetadata();
                if (response.Headers.TryGetValues("X-Pagination", out var paginationHeaderValues))
                {
                    metadata = JsonSerializer.Deserialize<PaginationMetadata>(
                        paginationHeaderValues.FirstOrDefault());
                }

                var customers = await response.Content.ReadFromJsonAsync<List<CustomerHiddenViewModel>>();
                return (customers, metadata);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching hidden customers");
                return (new List<CustomerHiddenViewModel>(), new PaginationMetadata());
            }
        }

        public async Task<(List<DeptCustomerViewModel> customers, PaginationMetadata metadata)> GetCustomerRemainTotalAsync(int pageNumber = 1,int pageSize = 20,string searchTerm = null)
        {
            try
            {
                var apiUrl = $"api/CustomerTracker/GetCostumerRemainTotal?pageNumber={pageNumber}&pageSize={pageSize}";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    apiUrl += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
                }

                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API returned {response.StatusCode} when fetching customer remain total");
                    return (new List<DeptCustomerViewModel>(), new PaginationMetadata());
                }

                var metadata = new PaginationMetadata();
                if (response.Headers.TryGetValues("X-Pagination", out var paginationHeaderValues))
                {
                    metadata = JsonSerializer.Deserialize<PaginationMetadata>(
                        paginationHeaderValues.FirstOrDefault());
                }

                var customers = await response.Content.ReadFromJsonAsync<List<DeptCustomerViewModel>>();
                return (customers, metadata);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching customer remain total");
                return (new List<DeptCustomerViewModel>(), new PaginationMetadata());
            }
        }

        public async Task<(List<InvoiceViewModel> invoices, PaginationMetadata metadata)> GetUncompletedInvoicesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = null)
        {
            try
            {
                var apiUrl = $"api/Invoice/GetUncompleteInvoices?pageNumber={pageNumber}&pageSize={pageSize}";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    apiUrl += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
                }

                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API returned {response.StatusCode} when fetching uncompleted invoices");
                    return (new List<InvoiceViewModel>(), new PaginationMetadata());
                }

                var metadata = new PaginationMetadata();
                if (response.Headers.TryGetValues("X-Pagination", out var paginationHeaderValues))
                {
                    metadata = JsonSerializer.Deserialize<PaginationMetadata>(
                        paginationHeaderValues.FirstOrDefault());
                }

                var invoices = await response.Content.ReadFromJsonAsync<List<InvoiceViewModel>>();
                return (invoices, metadata);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching uncompleted invoices");
                return (new List<InvoiceViewModel>(), new PaginationMetadata());
            }
        }
    }
}
