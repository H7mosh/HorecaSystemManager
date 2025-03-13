using System.Net.Http.Headers;
using System.Text;
using Hangfire;
using Newtonsoft.Json;
using Pipelines.Sockets.Unofficial.Arenas;
using sacmy.Server.Service;
using sacmy.Shared.ViewModels.Notification;


public class NotificationService
{
    private readonly IConfiguration _configuration;
    private readonly string _configKey;
    private readonly GoogleCredentialProvider _credentialProvider;
    private readonly ILogger<NotificationService> _logger;

    private readonly string _employeeProjectId;
    private readonly string _defaultProjectId;

    public NotificationService(IConfiguration configuration, string configurationKey, ILogger<NotificationService> logger)
    {
        _configuration = configuration;
        _configKey = configurationKey;
        _logger = logger;
        _credentialProvider = new GoogleCredentialProvider(configuration);

        try
        {
            var defaultConfig = configuration.GetSection("SafinAhmedNotificationKeys");
            var employeeConfig = configuration.GetSection("SafinAhmedManagerNotificationKeys");

            _defaultProjectId = defaultConfig["project_id"];
            _employeeProjectId = employeeConfig["project_id"];

            if (string.IsNullOrEmpty(_defaultProjectId) || string.IsNullOrEmpty(_employeeProjectId))
            {
                _logger.LogError("One or both Firebase project IDs are missing in the configuration.");
                throw new InvalidOperationException("Firebase project_id not found in configuration.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing NotificationService");
            throw;
        }
    }

    public async Task SendNotificationAsync(NotificationPayload payload, List<string> firebaseTokens)
    {
        _logger.LogInformation($"Starting to send notifications to {firebaseTokens.Count} recipients");

        try
        {
            var accessToken = await _credentialProvider.GetAccessTokenAsync(payload.IsEmployeeNotification);
            _logger.LogInformation("Successfully obtained access token");

            foreach (var token in firebaseTokens)
            {
                try
                {
                    _logger.LogInformation($"Attempting to send notification to token: {token.Substring(0, 6)}...");
                    await SendSingleNotificationAsync(token, payload, accessToken);
                    _logger.LogInformation($"Successfully sent notification to token: {token.Substring(0, 6)}...");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to send notification to token {token.Substring(0, 6)}...");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendNotificationAsync");
            throw;
        }
    }

    private async Task SendSingleNotificationAsync(string token, NotificationPayload payload, string accessToken)
    {
        string projectId = payload.IsEmployeeNotification ? _employeeProjectId : _defaultProjectId;


        var messagePayload = new
        {
            message = new
            {
                token = token,
                notification = new
                {
                    title = payload.Title,
                    body = payload.Body
                },
                android = new
                {
                    notification = new
                    {
                        sound = payload.Sound ?? "default"
                    }
                },
                apns = new
                {
                    payload = new
                    {
                        aps = new
                        {
                            sound = payload.Sound ?? "default"
                        }
                    }
                },
                data = new
                {
                    type = payload.Type,
                    message = payload.Message,
                    isEmployee = payload.IsEmployeeNotification.ToString()
                }
            }
        };

        using var client = new HttpClient();
        var url = $"https://fcm.googleapis.com/v1/projects/{projectId}/messages:send";

        _logger.LogInformation($"Sending FCM request to: {url}");

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(
                JsonConvert.SerializeObject(messagePayload),
                Encoding.UTF8,
                "application/json"
            )
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"FCM request failed with status {response.StatusCode}: {content}");
            throw new Exception($"FCM request failed: {content}");
        }

        _logger.LogInformation($"FCM request successful: {content}");
    }

    public async Task<string> ScheduleNotification(NotificationPayload payload, List<string> firebaseTokens, DateTime scheduledTime)
    {
        try
        {
            if (scheduledTime <= DateTime.Now)
            {
                throw new ArgumentException("Scheduled time must be in the future");
            }

            // Create a job with retry attempts
            var jobId = BackgroundJob.Schedule(
                () => SendNotificationWithRetry(payload, firebaseTokens),
                scheduledTime
            );

            _logger.LogInformation($"Notification scheduled. JobId: {jobId}, ScheduledTime: {scheduledTime}, Recipients: {firebaseTokens.Count}");

            return jobId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to schedule notification");
            throw;
        }
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task SendNotificationWithRetry(NotificationPayload payload, List<string> firebaseTokens)
    {
        try
        {
            _logger.LogInformation("Starting scheduled notification send with retry");
            await SendNotificationAsync(payload, firebaseTokens);
            _logger.LogInformation("Successfully completed scheduled notification send");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendNotificationWithRetry");
            throw; // Rethrowing to allow Hangfire to retry
        }
    }
}

