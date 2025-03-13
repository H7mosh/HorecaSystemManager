using System.Net.Http.Json;

namespace sacmy.Client.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _httpClient;

        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
    }
}
