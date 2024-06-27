using Microsoft.JSInterop;
using sacmy.Shared.ViewModels.TasksViewModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace sacmy.Client.Services
{
    public class TaskService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaskService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GetTaskViewModel>> GetTasksAsync()
        {
            return await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskViewModel>>("api/Tasks");
        }

        public async Task<List<GetTaskNotes>> GetTaskNotesAsync(string taskId)
        {
            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskNotes>>($"api/tasks/GetTaskNotes?taskId={taskId}");
            return response ?? new List<GetTaskNotes>();
        }

        public async Task<PostTaskNoteViewModel> PostTaskNoteAsync(PostTaskNoteViewModel model, string taskTitle)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").PostAsync($"api/tasks/PostTaskNote?taskTitle={taskTitle}", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PostTaskNoteViewModel>();
        }

    }

}
