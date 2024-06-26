using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class NotificationService
{
    private readonly IConfiguration _configuration;
    private readonly string _firebaseServerKey;
    private readonly string safenAhmedManagerServerKey;
    private readonly string _fcmUrl = "https://fcm.googleapis.com/fcm/send";

    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration;
        _firebaseServerKey = _configuration["Firebase:ServerKey"];
        safenAhmedManagerServerKey = _configuration["Firebase:SafinAhmedManagerServerKey"];
    }

    public async Task SendNotificationAsync(string title, string body, List<string> firebaseTokens , bool employeeNotification)
    {
        var notificationData = new
        {
            registration_ids = firebaseTokens,
            notification = new
            {
                title,
                body,
                content_available = true,
                mutable_content = true,
                sound = "ringtone.wav",
                playSound = true
            }
        };

        string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(notificationData);

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_fcmUrl);
            request.Method = "POST";
            if (employeeNotification)
            {
                request.Headers.Add("Authorization", "key=" + safenAhmedManagerServerKey);
            }
            else
            {
                request.Headers.Add("Authorization", "key=" + _firebaseServerKey);
            }
            request.ContentType = "application/json";

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string responseContent = await reader.ReadToEndAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Bulk notification sent successfully.");
                }
                else
                {
                    Console.WriteLine("Error sending bulk notification. Status code: " + response.StatusCode);
                    Console.WriteLine("Response Content:");
                    Console.WriteLine(responseContent);
                }
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine("WebException: " + webEx.Message);
            if (webEx.Response != null)
            {
                using (var errorResponse = (HttpWebResponse)webEx.Response)
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    string errorText = await reader.ReadToEndAsync();
                    Console.WriteLine("Error Response: " + errorText);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
