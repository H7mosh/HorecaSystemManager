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

        public async Task<List<GetTaskViewModel>> GetTasksAsync(Guid userId)
        {
            var query = $"api/Tasks?UserId={userId}";
            return await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskViewModel>>(query);
        }

        public async Task<List<GetTaskViewModel>> GetTasksByOrderOrCustomerAsync(Guid userId, int? customerId, int? invoiceId)
        {
            var query = $"api/Tasks/GetTasksByInvoiceIdOrCustomerId?UserId={userId}&CustomerId={customerId}&InvoiceId={invoiceId}";
            return await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskViewModel>>(query);
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

        public async Task<List<GetTaskStatus>> GetTaskStatusAsync()
        {
            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskStatus>>("api/Tasks/GetTaskStatus");
            return response ?? new List<GetTaskStatus>();
        }

        public async Task<List<GetTaskType>> GetTaskTypesAsync()
        {
            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskType>>("api/Tasks/GetTaskType");
            return response ?? new List<GetTaskType>();
        }

        public async Task<UpdateTaskViewModel> UpdateTaskAsync(UpdateTaskViewModel taskViewModel)
        {
            var jsonContent = JsonSerializer.Serialize(taskViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").PostAsync("api/Tasks/UpdateTask", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UpdateTaskViewModel>();
        }

        public async Task PostTaskAsync(PostTaskViewModel postTaskViewModel)
        {
            var jsonContent = JsonSerializer.Serialize(postTaskViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").PostAsync("api/Tasks", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<GetTaskViewModel>> GetTasksByOrderOrCustomerAsync(int userId, int? customerId, int? invoiceId)
        {
            try
            {
                string url = "api/Task/GetTasksByOrderOrCustomer";

                // Add query parameters
                var queryParams = new List<string>();
                queryParams.Add($"userId={userId}");

                if (customerId.HasValue)
                {
                    queryParams.Add($"customerId={customerId.Value}");
                }

                if (invoiceId.HasValue)
                {
                    queryParams.Add($"invoiceId={invoiceId.Value}");
                }

                // Combine query parameters
                url = $"{url}?{string.Join("&", queryParams)}";

                var response =  await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetTaskViewModel>>(url);

                return response;
            }
            catch (Exception ex)
            {
                
                return new List<GetTaskViewModel>();
            }
        }

    }

}
