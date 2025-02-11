 using System.Net.Http.Json;
using sacmy.Shared.ViewModels.InvoiceViewModel;
using sacmy.Shared.ViewModels.OrdersViewModel;

namespace sacmy.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrdersByStagesViewModel>> GetStagesAsync()
        {
            var response = await _httpClient.GetAsync("api/Orders/stages");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<OrdersByStagesViewModel>>() ?? new();
            }
            return new();
        }

        public async Task<PaginatedResponse<OrderViewModel>> GetOrdersByStageAsync(PaginationFilter filter)
        {
            var queryString = BuildQueryString(filter);
            var response = await _httpClient.GetAsync($"api/Orders/orders-by-stage{queryString}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PaginatedResponse<OrderViewModel>>() ?? new();
            }
            return new();
        }

        public async Task<List<OrderItemViewModel>> GetOrderItemsAsync(int orderId)
        {
            var response = await _httpClient.GetAsync($"api/Orders/{orderId}/items");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<OrderItemViewModel>>() ?? new();
            }
            return new();
        }

        public async Task<Dictionary<string, List<OrderViewerViewModel>>> FilterOrderAsync(int orderId, List<StorageOrder> storageOrder, List<OrderItemViewModel> orderItems)
        {
            var request = new OrderToFilter
            {
                onlineOrderItemsToFilter = orderItems.Select(item => new OnlineOrderItemViewModelToFilter
                {
                    OrderId = orderId,
                    Sku = item.Sku,
                    Qtty = item.Quantity,
                    Item = item.PatternCode,
                    Price = item.Price
                }).ToList(),
                storageOrderList = storageOrder
            };

            var response = await _httpClient.PostAsJsonAsync("api/Orders/filterOrder", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Dictionary<string, List<OrderViewerViewModel>>>() ?? new();
            }

            throw new Exception($"Failed to filter order: {response.ReasonPhrase}");
        }

        private static string BuildQueryString(PaginationFilter filter)
        {
            var queryParams = new List<string>
            {
                $"pageNumber={filter.PageNumber}",
                $"pageSize={filter.PageSize}"
            };

            if (!string.IsNullOrEmpty(filter.SearchTerm))
                queryParams.Add($"searchTerm={Uri.EscapeDataString(filter.SearchTerm)}");

            if (filter.StartDate.HasValue)
                queryParams.Add($"startDate={filter.StartDate.Value:yyyy-MM-dd}");

            if (filter.EndDate.HasValue)
                queryParams.Add($"endDate={filter.EndDate.Value:yyyy-MM-dd}");

            if (filter.OrderStageId.HasValue)
                queryParams.Add($"orderStageId={filter.OrderStageId}");

            return queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        }

        public async Task<bool> CreateInvoiceAsync(InvoiceFromOrderViewModel invoiceModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Orders/createinvoice", invoiceModel);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception($"Failed to create invoice: {response.ReasonPhrase}");
        }

        public async Task<bool> UpdateOrderStageAsync(int orderId, Guid stageId, int invoiceId, bool isItTheMainInvoice)
        {
            var queryString = $"?orderId={orderId}&stageId={stageId}&invoiceId={invoiceId}&isItTheMainInvoice={isItTheMainInvoice}";
            var response = await _httpClient.PostAsync($"api/Orders/update-order-stage{queryString}", null);
            if (response.IsSuccessStatusCode)
            {
                return true; // Successfully updated
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to update order stage: {response.ReasonPhrase}. Details: {errorMessage}");
        }

        public async Task<List<InvoiceItemsViewModel>> GetMultipleOrderItemsAsync(List<int> orderIds)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Invoice/GetMultipleInvoicesItems", orderIds);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<InvoiceItemsViewModel>>() ?? new();
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to get multiple order items: {response.ReasonPhrase}. Details: {errorMessage}");
        }
    }
}
