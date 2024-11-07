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

    // This method sends the notification request to the server-side API
    public async Task SendNotificationAsync(NotificationRequestViewModel request)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // This URL is relative to the server-side API
            var response = await _httpClient.PostAsync("api/Notification/sendOrSchedule", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification: {ex.Message}");
        }
    }

    // This method sends the scheduled notification request to the server-side API
    public async Task ScheduleNotificationAsync(ScheduledNotificationRequestViewModel request)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // This URL is relative to the server-side API
            var response = await _httpClient.PostAsync("api/Notification/sendOrSchedule", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scheduling notification: {ex.Message}");
        }
    }
}
