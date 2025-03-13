using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.CustomerTracker;
using sacmy.Shared.ViewModels.StickNoteViewModel;
using System.Net.Http.Json;

namespace sacmy.Client.Services
{
    public class CustomerStickyNoteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerStickyNoteService> _logger;

        public CustomerStickyNoteService(HttpClient httpClient, ILogger<CustomerStickyNoteService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<GetStickyNoteViewModel>> CreateStickyNoteAsync(AddCustomerStickyNoteViewModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/StickyNotes", model);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<GetStickyNoteViewModel>>();
                    return result;
                }
                else
                {
                    return new ApiResponse<GetStickyNoteViewModel>
                    {
                        Success = false,
                        Message = $"Error creating sticky note: {response.ReasonPhrase}"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sticky note");
                return new ApiResponse<GetStickyNoteViewModel>
                {
                    Success = false,
                    Message = $"Error creating sticky note: {ex.Message}"
                };
            }
        }

        public async Task<List<GetStickyNoteViewModel>> GetNotesByCustomerAsync(int customerId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<GetStickyNoteViewModel>>>($"api/CustomerTracker/GetCustomerStickyNotes?customerId={customerId}");
                return response?.Data ?? new List<GetStickyNoteViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sticky notes for customer");
                return new List<GetStickyNoteViewModel>();
            }
        }

        public async Task<List<GetStickyNoteViewModel>> GetNotesByInvoiceAsync(int invoiceId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<GetStickyNoteViewModel>>>($"api/CustomerTracker/GetInvoiceStickyNotes?invoiceId={invoiceId}");
                return response?.Data ?? new List<GetStickyNoteViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sticky notes for invoice");
                return new List<GetStickyNoteViewModel>();
            }
        }
    }
}
