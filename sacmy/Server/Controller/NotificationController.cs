using Microsoft.AspNetCore.Mvc;
using sacmy.Shared.ViewModels.Notification;

namespace sacmy.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
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

            // Check if the request is for a scheduled notification
            if (request.ScheduleTime.HasValue && request.ScheduleTime > DateTime.Now)
            {
                // Schedule the notification for a future time
                await _notificationService.ScheduleNotification(request.Title, request.Body, request.FirebaseTokens, request.employeeNotification, request.ScheduleTime.Value);
                return Ok("Notification scheduled successfully.");
            }
            else
            {
                // Send the notification immediately
                await _notificationService.SendNotificationAsync(request.Title, request.Body, request.FirebaseTokens, request.employeeNotification);
                return Ok("Notification sent successfully.");
            }
        }

    }

}
