using sacmy.Shared.ViewModels.CustomerTracker;
using sacmy.Shared.ViewModels.InvoiceViewModel;
using System.Net.Http.Json;

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

        public async Task<List<CustomerHiddenViewModel>> GetHiddenCustomersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CustomerHiddenViewModel>>("api/CustomerTracker/GetHiddenCustomer");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching hidden customers");
                return new List<CustomerHiddenViewModel>();
            }
        }

        public async Task<List<DeptCustomerViewModel>> GetCustomerRemainTotalAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DeptCustomerViewModel>>("api/CustomerTracker/GetCostumerRemainTotal");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching customer remain total");
                return new List<DeptCustomerViewModel>();
            }
        }

        public async Task<List<InvoiceViewModel>> GetUncompletedInvoicesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<InvoiceViewModel>>("api/Invoice/GetUncompleteInvoices");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching uncompleted invoices");
                return new List<InvoiceViewModel>();
            }
        }
    }
}
