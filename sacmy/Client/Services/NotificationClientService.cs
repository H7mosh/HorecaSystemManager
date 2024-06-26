using Newtonsoft.Json;
using sacmy.Shared.ViewModels.Notification;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

public class NotificationClientService
{
    private readonly HttpClient _httpClient;

    public NotificationClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendNotificationAsync(NotificationRequestViewModel request)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Notification/send", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex) { 
            
        }
    }
}