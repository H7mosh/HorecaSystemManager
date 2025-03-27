using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.AdvertiseVewModels;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace sacmy.Client.Services
{
    public class AdvertiseService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AdvertiseService> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AdvertiseService(HttpClient httpClient, ILogger<AdvertiseService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Get all advertisements
        public async Task<List<GetAdvertiseViewModel>> GetAllAdvertisementsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<GetAdvertiseViewModel>>>("api/Advertise");

                if (response != null && response.Success)
                {
                    return new List<GetAdvertiseViewModel>(response.Data);
                }

                _logger.LogWarning($"Failed to get advertisements: {response?.Message}");
                return new List<GetAdvertiseViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting advertisements");
                throw;
            }
        }

        // Get advertisement by ID
        public async Task<GetAdvertiseViewModel> GetAdvertisementByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<GetAdvertiseViewModel>>($"api/Advertise/{id}");

                if (response != null && response.Success)
                {
                    return response.Data;
                }

                _logger.LogWarning($"Failed to get advertisement with ID {id}: {response?.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting advertisement with ID {id}");
                throw;
            }
        }

        // Create advertisement
        public async Task<ApiResponse<GetAdvertiseViewModel>> CreateAdvertisementAsync(CreateAdvertiseViewModel model)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsync(
                    "api/Advertise",
                    JsonContent.Create(model));

                var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<GetAdvertiseViewModel>>(_jsonOptions);

                if (response != null && !response.Success)
                {
                    _logger.LogWarning($"Failed to create advertisement: {response.Message}");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating advertisement");
                throw;
            }
        }

        // Update advertisement
        public async Task<ApiResponse> UpdateAdvertisementAsync(Guid id, CreateAdvertiseViewModel model)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsync(
                    $"api/Advertise/Update/{id}",
                    JsonContent.Create(model));

                var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse>(_jsonOptions);

                if (response != null && !response.Success)
                {
                    _logger.LogWarning($"Failed to update advertisement with ID {id}: {response.Message}");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating advertisement with ID {id}");
                throw;
            }
        }

        // Delete advertisement
        public async Task<ApiResponse> DeleteAdvertisementAsync(Guid id)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsync(
                    $"api/Advertise/Delete/{id}",
                    new StringContent(string.Empty, Encoding.UTF8, "application/json"));

                var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse>(_jsonOptions);

                if (response != null && !response.Success)
                {
                    _logger.LogWarning($"Failed to delete advertisement with ID {id}: {response.Message}");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting advertisement with ID {id}");
                throw;
            }
        }

        public async Task<ApiResponse> UploadAdvertisementImageAsync(MultipartFormDataContent formData)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsync("api/Advertise/UploadImage", formData);

                var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse>(_jsonOptions);

                if (response != null && !response.Success)
                {
                    _logger.LogWarning($"Failed to upload advertisement image: {response.Message}");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading advertisement image");
                throw;
            }
        }
    }
}
