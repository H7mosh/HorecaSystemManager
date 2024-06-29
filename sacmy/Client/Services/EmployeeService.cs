using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.TasksViewModel;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace sacmy.Client.Services
{
    public class EmployeeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GetEmployeeViewModel>> GetEmployeesAsync()
        {
            var response = await _httpClientFactory.CreateClient("sacmy.ServerAPI").GetFromJsonAsync<List<GetEmployeeViewModel>>("api/Employee");
            return response ?? new List<GetEmployeeViewModel>();
        }

    }
}
