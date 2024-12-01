using Microsoft.AspNetCore.Mvc;
using sacmy.Shared.ViewModels.Notification;
using Microsoft.Extensions.Configuration;

namespace sacmy.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private NotificationService _managerNotificationService;
        private NotificationService _customerNotificationService;
        private readonly IConfiguration _configuration;

        public NotificationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _managerNotificationService = new NotificationService(configuration, "SafinAhmedManagerNotificationKeys");
            _customerNotificationService = new NotificationService(configuration, "SafinAhmedNotificationKeys");
        }

        // API to send immediate notification
        [HttpPost("sendOrSchedule")]
        public async Task<IActionResult> SendOrScheduleNotification([FromBody] ScheduledNotificationRequestViewModel request)
        {
            // Validate the request
            if (request == null || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Body) || request.FirebaseTokens == null || !request.FirebaseTokens.Any())
            {
                return BadRequest("Invalid request.");
            }

            // Choose the appropriate notification service based on whether it's an employee notification
            var notificationService = request.employeeNotification ? _managerNotificationService : _customerNotificationService;

            // Check if the request is for a scheduled notification
            if (request.ScheduleTime.HasValue && request.ScheduleTime > DateTime.Now)
            {
                // Schedule the notification for a future time
                await notificationService.ScheduleNotification(request.Title, request.Body, request.FirebaseTokens, request.employeeNotification, request.ScheduleTime.Value);
                return Ok("Notification scheduled successfully.");
            }
            else
            {
                // Send the notification immediately
                await notificationService.SendNotificationAsync(request.Title, request.Body, request.FirebaseTokens, request.employeeNotification);
                return Ok("Notification sent successfully.");
            }
        }

    }

}
