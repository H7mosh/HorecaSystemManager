using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.StoryViewModel;
using System.Net.Http.Json;

namespace sacmy.Client.Services
{
    public class StoryService
    {
        private readonly HttpClient _httpClient;

        public StoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GetStoryViewModel>> GetAllStoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<GetStoryViewModel>>>("api/Story");
            return response?.Data != null ? new List<GetStoryViewModel>(response.Data) : new List<GetStoryViewModel>();
        }

        public async Task<GetStoryViewModel> GetStoryByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<GetStoryViewModel>>($"api/Story/{id}");
            return response?.Data;
        }

        public async Task<ApiResponse<GetStoryViewModel>> CreateStoryAsync(CreateStoryViewModel story)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Story", story);
            return await response.Content.ReadFromJsonAsync<ApiResponse<GetStoryViewModel>>();
        }

        public async Task<ApiResponse> UpdateStoryAsync(Guid id, UpdateStoryViewModel story)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Story/Update/{id}", story);
            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<ApiResponse> DeleteStoryAsync(Guid id)
        {
            var response = await _httpClient.PostAsync($"api/Story/Delete/{id}", null);
            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<ApiResponse> UploadStoryMediaAsync(MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync("api/Story/UploadMedia", content);
            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<ApiResponse> AddStoryViewAsync(AddStoryViewModel viewData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Story/AddView", viewData);
            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }
    }
}
