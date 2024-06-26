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

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequestViewModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Body) || request.FirebaseTokens == null || !request.FirebaseTokens.Any())
            {
                return BadRequest("Invalid request.");
            }

            await _notificationService.SendNotificationAsync(request.Title, request.Body, request.FirebaseTokens , request.employeeNotification);
            return Ok("Notification sent successfully.");
        }
    }
}
