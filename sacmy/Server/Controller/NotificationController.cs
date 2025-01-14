using Microsoft.AspNetCore.Mvc;
using sacmy.Shared.ViewModels.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.Models;
using sacmy.Server.DatabaseContext;

namespace sacmy.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _managerNotificationService;
        private readonly NotificationService _customerNotificationService;
        private readonly ILogger<NotificationService> _notificationLogger;
        private readonly SafeenCompanyDbContext _context;

        public NotificationController(IConfiguration configuration, ILogger<NotificationService> notificationLogger, SafeenCompanyDbContext context)
        {
            _notificationLogger = notificationLogger;
            _context = context;

            _managerNotificationService = new NotificationService(
                configuration,
                "SafinAhmedManagerNotificationKeys",
                _notificationLogger
            );

            _customerNotificationService = new NotificationService(
                configuration,
                "SafinAhmedNotificationKeys",
                _notificationLogger
            );
        }

        [HttpPost("sendOrSchedule")]
        public async Task<IActionResult> SendOrScheduleNotification([FromBody] NotificationRequest request)
        {
            try
            {
                if (!IsValidRequest(request))
                {
                    return BadRequest("Invalid request parameters.");
                }

                var payload = new NotificationPayload
                {
                    Title = request.Title,
                    Body = request.Description,
                    Type = request.Type,
                    Message = request.Message,
                    IsEmployeeNotification = request.Type.Equals("employee", StringComparison.OrdinalIgnoreCase)
                };

                var firebaseTokens = request.UserIdAndTokenList
                    .Select(x => x.FirebaseToken)
                    .ToList();

                var notificationService = payload.IsEmployeeNotification
                    ? _managerNotificationService
                    : _customerNotificationService;

                if (request.ScheduleTime.HasValue && request.ScheduleTime > DateTime.Now)
                {
                    var jobId = await notificationService.ScheduleNotification(
                        payload,
                        firebaseTokens,
                        request.ScheduleTime.Value
                    );

                    return Ok(new
                    {
                        message = "Notification scheduled successfully",
                        jobId = jobId,
                        scheduledTime = request.ScheduleTime.Value,
                        recipientCount = firebaseTokens.Count
                    });
                }

                await notificationService.SendNotificationAsync(payload, firebaseTokens);

                if (!payload.IsEmployeeNotification)
                {

                    //Create and save notification record
                    var notification = new Notification
                    {
                        Id = Guid.NewGuid(),
                        Title = request.Title,
                        Description = request.Description,
                        Type = request.Type,
                        Message = request.Message,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                    };

                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();

                    // Create and save user notifications
                    var userNotifications = request.UserIdAndTokenList.Select(user => new UserNotification
                    {
                        Id = Guid.NewGuid(),
                        NotificationId = notification.Id,
                        CustomerId = int.Parse(user.Id.ToString()),
                        Status = "unviewed",
                        CreatedDate = DateTime.Now,
                    }).ToList();

                    _context.UserNotifications.AddRange(userNotifications);
                    await _context.SaveChangesAsync();
                }

                return Ok("Notification sent successfully");
            }
            catch (Exception ex)
            {
                var error = $"Failed to process notification request: {ex.Message}";
                Console.WriteLine(error);
                return StatusCode(500, error);
            }
        }

        private bool IsValidRequest(NotificationRequest request)
        {
            return request != null
                && !string.IsNullOrEmpty(request.Title)
                && !string.IsNullOrEmpty(request.Description)
                && !string.IsNullOrEmpty(request.Type)
                && request.UserIdAndTokenList?.Any() == true;
        }
    }
}
