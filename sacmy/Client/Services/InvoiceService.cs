using sacmy.Shared.ViewModels.InvoiceViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace sacmy.Client.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InvoiceService> _logger;


        public InvoiceService(HttpClient httpClient , ILogger<InvoiceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> GenerateInvoiceReportAsync(int invoiceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Orders/generate-invoice-report/{invoiceId}");

                // If response is successful (2xx status code), return true
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                // If response is BadRequest (400), return false
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return false;
                }

                // For any other status code, throw an exception
                response.EnsureSuccessStatusCode();
                return false; // This line won't be reached due to EnsureSuccessStatusCode()
            }
            catch (HttpRequestException ex)
            {
                // Log the error or handle it as needed
                throw new HttpRequestException($"Failed to generate invoice report for invoice {invoiceId}", ex);
            }
        }

        public async Task<(List<InvoiceViewModel> Invoices, InvoicePaginationMetadataViewModel PaginationData)> GetInvoicesAsync(
           int pageNumber,
           int pageSize,
           bool? isCompleted = null)
        {
            try
            {
                _logger.LogInformation($"Requesting invoices: pageNumber={pageNumber}, pageSize={pageSize}, isCompleted={isCompleted}");

                // Build the API URL with query parameters
                var apiUrl = $"api/Invoice?pageNumber={pageNumber}&pageSize={pageSize}";
                if (isCompleted.HasValue)
                {
                    apiUrl += $"&isCompleted={isCompleted.Value}";
                }

                var response = await _httpClient.GetAsync(apiUrl);

                // Initialize pagination metadata
                var paginationMetadata = new InvoicePaginationMetadataViewModel();

                // Extract pagination data from header
                if (response.Headers.TryGetValues("X-Pagination", out var paginationHeaderValues))
                {
                    paginationMetadata = JsonSerializer.Deserialize<InvoicePaginationMetadataViewModel>(
                        paginationHeaderValues.FirstOrDefault());

                    _logger.LogInformation(
                        $"Pagination info: TotalCount={paginationMetadata.TotalCount}, " +
                        $"TotalPages={paginationMetadata.TotalPages}, " +
                        $"CurrentPage={paginationMetadata.CurrentPage}, " +
                        $"PageSize={paginationMetadata.PageSize}");
                }

                if (response.IsSuccessStatusCode)
                {
                    var invoices = await response.Content.ReadFromJsonAsync<List<InvoiceViewModel>>();
                    _logger.LogInformation($"Retrieved {invoices.Count} records from API");

                    return (invoices, paginationMetadata);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"API error status: {response.StatusCode}, Content: {errorContent}");

                    return (new List<InvoiceViewModel>(), paginationMetadata);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoices");
                throw;
            }
        }

    }
}
