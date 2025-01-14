using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using sacmy.Server.Service;
using sacmy.Shared.ViewModels.Notification;

public class NotificationService
{
    private readonly IConfiguration _configuration;
    private readonly string _configKey;
    private readonly string _projectId;
    private readonly GoogleCredentialProvider _credentialProvider;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IConfiguration configuration,string configurationKey,ILogger<NotificationService> logger)
    {
        _configuration = configuration;
        _configKey = configurationKey;
        _logger = logger;
        _credentialProvider = new GoogleCredentialProvider(configuration);

        try
        {
            var firebaseConfig = configuration.GetSection(configurationKey);
            _projectId = firebaseConfig["project_id"];

            if (string.IsNullOrEmpty(_projectId))
            {
                _logger.LogError($"Project ID is null or empty for config key: {configurationKey}");
                throw new InvalidOperationException($"Firebase project_id not found in configuration for key: {configurationKey}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error initializing NotificationService with key {configurationKey}");
            throw;
        }
    }

    public async Task SendNotificationAsync(NotificationPayload payload, List<string> firebaseTokens)
    {
        _logger.LogInformation($"Starting to send notifications to {firebaseTokens.Count} recipients");

        try
        {
            var accessToken = await _credentialProvider.GetAccessTokenAsync(_configKey);
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
        var url = $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send";

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

