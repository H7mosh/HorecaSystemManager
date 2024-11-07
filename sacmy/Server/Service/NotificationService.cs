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

public class NotificationService
{
    private readonly IConfiguration _configuration;
    private readonly string _privateKey;
    private readonly string _clientEmail;
    private readonly string _tokenUri;
    private readonly string _projectId;

    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration; 
        var firebaseConfig = _configuration.GetSection("SafinAhmedNotificationKeys");
        _privateKey = firebaseConfig["private_key"];
        _clientEmail = firebaseConfig["client_email"];
        _tokenUri = firebaseConfig["token_uri"];
        _projectId = firebaseConfig["project_id"];
    }

    // Generate the OAuth2 Access Token for FCM HTTP v1 API
    public async Task<string> GetAccessTokenAsync()
    {
        // Read all necessary fields from appsettings.json
        var firebaseConfig = _configuration.GetSection("SafinAhmedNotificationKeys"); // Retrieve configuration section
        var type = firebaseConfig["type"];
        var projectId = firebaseConfig["project_id"];
        var privateKeyId = firebaseConfig["private_key_id"];
        var privateKey = firebaseConfig["private_key"].Replace("\\n", "\n");
        var clientEmail = firebaseConfig["client_email"];
        var clientId = firebaseConfig["client_id"];
        var authUri = firebaseConfig["auth_uri"];
        var tokenUri = firebaseConfig["token_uri"];
        var authProviderX509CertUrl = firebaseConfig["auth_provider_x509_cert_url"];
        var clientX509CertUrl = firebaseConfig["client_x509_cert_url"];

        // Build the complete JSON structure required by GoogleCredential
        var credentialJson = $@"
                {{
                    ""type"": ""{type}"",
                    ""project_id"": ""{projectId}"",
                    ""private_key_id"": ""{privateKeyId}"",
                    ""private_key"": ""{privateKey}"",
                    ""client_email"": ""{clientEmail}"",
                    ""client_id"": ""{clientId}"",
                    ""auth_uri"": ""{authUri}"",
                    ""token_uri"": ""{tokenUri}"",
                    ""auth_provider_x509_cert_url"": ""{authProviderX509CertUrl}"",
                    ""client_x509_cert_url"": ""{clientX509CertUrl}""
                }}";

        // Use GoogleCredential to create the credentials
        var credential = GoogleCredential.FromJson(credentialJson)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        // Get the access token
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

        return accessToken;
    }

    // Method to send notification immediately
    public async Task SendNotificationAsync(string title, string body, List<string> firebaseTokens, bool employeeNotification)
    {
        var accessToken = await GetAccessTokenAsync();

        foreach (var token in firebaseTokens)
        {
            var messagePayload = new
            {
                message = new
                {
                    token = token,
                    notification = new
                    {
                        title = title,
                        body = body,
                    },
                    android = new
                    {
                        notification = new
                        {
                            sound = "reminder.wav" // Set custom sound for Android
                        }
                    },
                    apns = new
                    {
                        payload = new
                        {
                            aps = new
                            {
                                sound = "reminder.wav" // Set custom sound for iOS
                            }
                        }
                    },
                    data = new
                    {
                        type = employeeNotification ? "employee" : "regular",
                        message = "Custom data based on employeeNotification"
                    }
                }
            };


            var jsonMessage = JsonConvert.SerializeObject(messagePayload);

            // Log the payload for debugging
            Console.WriteLine("Payload: " + jsonMessage);

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send")
            {
                Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.SendAsync(request);

            // Log the response for debugging
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response: " + responseContent);

            response.EnsureSuccessStatusCode();
        }
    }

    // Schedule a notification using Hangfire
    public async Task ScheduleNotification(string title, string body, List<string> firebaseTokens, bool employeeNotification, DateTime scheduledTime)
    {
        // Schedule the job to run at the specified time
        BackgroundJob.Schedule(() => SendScheduledNotification(title, body, firebaseTokens, employeeNotification), scheduledTime);

        await Task.CompletedTask; // Since BackgroundJob.Schedule is non-blocking, just return a completed task.
    }

    // This method will be called by Hangfire when it's time to send the notification
    public async Task SendScheduledNotification(string title, string body, List<string> firebaseTokens, bool employeeNotification)
    {
        await SendNotificationAsync(title, body, firebaseTokens, employeeNotification);
    }

}



