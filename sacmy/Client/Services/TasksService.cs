using sacmy.Shared.ViewModels.TasksViewModel;
using System.Net.Http;
using System.Net.Http.Json;

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
    }
}
