using sacmy.Client.Configuraion;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.CustomerViewModel;
using System.Net.Http.Json;

namespace sacmy.Client.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<CustomerViewModel>>> GetCustomersAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CustomerViewModel>>>($"api/Customer/GetCustomers?pageNumber={pageNumber}&pageSize={pageSize}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching customers: {ex.Message}");
                return new ApiResponse<List<CustomerViewModel>>
                {
                    Success = false,
                    Message = $"Error fetching customers: {ex.Message}",
                    Data = new List<CustomerViewModel>()
                };
            }
        }

        public async Task<ApiResponse<List<CustomerViewModel>>> SearchCustomersAsync(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CustomerViewModel>>>($"api/Customer/SearchCustomers?searchQuery={Uri.EscapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching customers: {ex.Message}");
                return new ApiResponse<List<CustomerViewModel>>
                {
                    Success = false,
                    Message = $"Error searching customers: {ex.Message}",
                    Data = new List<CustomerViewModel>(),
                    TotalCount = 0
                };
            }
        }

        public async Task<ApiResponse<CustomerViewModel>> CreateCustomerAsync(CustomerViewModel customer)
        {
            try
            {
                var apiUrl = $"api/Customer";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, customer);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerViewModel>>();
                    return result ?? new ApiResponse<CustomerViewModel>
                    {
                        Success = true,
                        Message = "Customer created successfully",
                        Data = customer
                    };
                }

                return new ApiResponse<CustomerViewModel>
                {
                    Success = false,
                    Message = $"Failed to create customer. Status code: {response.StatusCode}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CustomerViewModel>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<CustomerViewModel>> UpdateCustomerAsync(CustomerViewModel customer)
        {
            try
            {
                // Changed from PUT to POST to match the new endpoint
                var apiUrl = $"api/Customer/Update/{customer.Id}";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, customer);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerViewModel>>();
                    return result ?? new ApiResponse<CustomerViewModel>
                    {
                        Success = true,
                        Message = "Customer updated successfully",
                        Data = customer
                    };
                }

                return new ApiResponse<CustomerViewModel>
                {
                    Success = false,
                    Message = $"Failed to update customer. Status code: {response.StatusCode}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CustomerViewModel>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<object>> BulkAddCustomerProductRelationsAsync(List<CustomerProductRelationViewModel> relations)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Customer/BulkAddCustomerProductRelations", relations);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                    return result ?? new ApiResponse<object>
                    {
                        Success = true,
                        Message = "Custom pricing applied successfully"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Failed to apply custom pricing: {errorContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating customer product relations: {ex.Message}");
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error creating customer product relations: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List<CustomerProductPriceChangeViewModel>>> GetCustomerProductPriceChangesAsync(Guid productId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CustomerProductPriceChangeViewModel>>>($"api/Customer/GetCustomerProductPriceChanges/{productId}");
                return response ?? new ApiResponse<List<CustomerProductPriceChangeViewModel>>
                {
                    Success = false,
                    Message = "No response from server",
                    Data = new List<CustomerProductPriceChangeViewModel>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product price changes: {ex.Message}");
                return new ApiResponse<List<CustomerProductPriceChangeViewModel>>
                {
                    Success = false,
                    Message = $"Error fetching product price changes: {ex.Message}",
                    Data = new List<CustomerProductPriceChangeViewModel>()
                };
            }
        }

        public async Task<ApiResponse<bool>> SendNotificationToCustomerAsync(string customerName)
        {
            try
            {
                var apiUrl = $"api/Customer/SendNotification/{Uri.EscapeDataString(customerName)}";
                var response = await _httpClient.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<bool>
                    {
                        Success = true,
                        Message = "Notification sent successfully",
                        Data = true
                    };
                }

                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Failed to send notification. Status code: {response.StatusCode}",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    Data = false
                };
            }
        }

    }
}
