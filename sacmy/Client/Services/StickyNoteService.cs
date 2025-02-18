using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.StickNoteViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace sacmy.Client.Services
{
    public class StickyNoteService
    {
        private readonly HttpClient _httpClient;

        public StickyNoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. Create a new sticky note
        public async Task<ApiResponse<GetStickyNoteViewModel>> CreateStickyNoteAsync(AddStickyNoteViewModel model)
        {
            // Call POST /api/StickyNotes
            var response = await _httpClient.PostAsJsonAsync("api/StickyNotes", model);

            // Optionally, handle non-success status codes in a custom way
            if (!response.IsSuccessStatusCode)
            {
                // Attempt to read the error response as an ApiResponse or fallback
                var errorString = await response.Content.ReadAsStringAsync();
                try
                {
                    var apiError = JsonSerializer.Deserialize<ApiResponse>(errorString);
                    return new ApiResponse<GetStickyNoteViewModel>
                    {
                        Success = false,
                        Message = apiError?.Message ?? "Unknown error",
                        Data = null
                    };
                }
                catch
                {
                    // If we can't deserialize, just return a generic error
                    return new ApiResponse<GetStickyNoteViewModel>
                    {
                        Success = false,
                        Message = $"HTTP error: {response.StatusCode}",
                        Data = null
                    };
                }
            }

            // If Success, read content as ApiResponse<GetStickyNoteViewModel>
            var createdApiResponse = await response.Content
                .ReadFromJsonAsync<ApiResponse<GetStickyNoteViewModel>>();

            return createdApiResponse;
        }

        // 2. Get a sticky note by ID
        public async Task<ApiResponse<GetStickyNoteViewModel>> GetStickyNoteByIdAsync(Guid id)
        {
            // Call GET /api/StickyNotes/{id}
            var response = await _httpClient.GetAsync($"api/StickyNotes/{id}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                var errorString = await response.Content.ReadAsStringAsync();
                try
                {
                    var apiError = JsonSerializer.Deserialize<ApiResponse>(errorString);
                    return new ApiResponse<GetStickyNoteViewModel>
                    {
                        Success = false,
                        Message = apiError?.Message ?? "Unknown error",
                        Data = null
                    };
                }
                catch
                {
                    return new ApiResponse<GetStickyNoteViewModel>
                    {
                        Success = false,
                        Message = $"HTTP error: {response.StatusCode}",
                        Data = null
                    };
                }
            }

            // If Success
            var apiResponse = await response.Content
                .ReadFromJsonAsync<ApiResponse<GetStickyNoteViewModel>>();

            return apiResponse;
        }

        public async Task<ApiResponse<List<GetStickyNoteViewModel>>> GetNotesByRecordAsync(string tableName, Guid recordId)
        {
            var response = await _httpClient.GetAsync($"api/StickyNotes/record?tableName={tableName}&recordId={recordId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                try
                {
                    var apiError = JsonSerializer.Deserialize<ApiResponse>(errorString);
                    return new ApiResponse<List<GetStickyNoteViewModel>>
                    {
                        Success = false,
                        Message = apiError?.Message ?? "Unknown error",
                        Data = null
                    };
                }
                catch
                {
                    return new ApiResponse<List<GetStickyNoteViewModel>>
                    {
                        Success = false,
                        Message = $"HTTP error: {response.StatusCode}",
                        Data = null
                    };
                }
            }

            var apiResponse = await response.Content
                .ReadFromJsonAsync<ApiResponse<List<GetStickyNoteViewModel>>>();
            return apiResponse;
        }
    }
}
