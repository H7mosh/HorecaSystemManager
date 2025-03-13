using Dapper;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sacmy.Shared.ViewModels.Notification;
using sacmy.Shared.ViewModels.LowStockViewModels;

namespace sacmy.Server.Service
{
    public class LowStockNotificationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _connectionString;
        private readonly ILogger<LowStockNotificationService> _logger;

        public LowStockNotificationService(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            ILogger<LowStockNotificationService> logger = null)
        {
            _serviceProvider = serviceProvider;
            _connectionString = configuration.GetConnectionString("onlineConnectionString");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAndSendNotifications();
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error occurred while checking and sending low stock notifications");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Runs every 5 minutes
            }
        }

        public async Task CheckAndSendNotifications()
        {
            LogInformation("Starting low stock notification check");

            using (var scope = _serviceProvider.CreateScope())
            {
                var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    // Use regular Open() method instead of OpenAsync()
                    db.Open();

                    try
                    {
                        // Get all unprocessed notifications
                        var query = @"
                        SELECT 
                        ln.ID, 
                        ln.ProductID, 
                        k.Name AS ProductName, 
                        ln.EmployeeID, 
                        (k.Qtty_Purch - k.Qtty_Sales + k.Qtty_Return) AS Quantity, 
                        e.FirebaseToken,
                        r.Threshold, 
                        ln.CreatedDate, 
                        ln.IsProcessed, 
                        ln.IsDeleted
                    FROM LowStockNotifications ln
                    JOIN KP_Store k ON ln.ProductID = k.Id
                    JOIN Employee e ON ln.EmployeeID = e.Id
                    JOIN LowStockRecipients r ON ln.ProductID = r.ProductID AND ln.EmployeeID = r.EmployeeID
                    WHERE ln.IsProcessed = 0 AND ln.IsDeleted = 0";

                        var notifications = await db.QueryAsync<NotificationItem>(query);

                        LogInformation($"Found {notifications.Count()} unprocessed notifications");

                        foreach (var notification in notifications)
                        {
                            LogInformation($"Processing notification for product {notification.ProductName} (ID: {notification.ProductID})");

                            if (!string.IsNullOrEmpty(notification.FirebaseToken))
                            {
                                var payload = new NotificationPayload
                                {
                                    Title = "⚠️ Low Stock Alert",
                                    Body = $"{notification.ProductName} has only {notification.Quantity} left (threshold: {notification.Threshold})!",
                                    Type = "low_stock",
                                    Message = $"The product {notification.ProductName} is running low on stock.",
                                    IsEmployeeNotification = true
                                };

                                try
                                {
                                    // Send notification to employee
                                    await notificationService.SendNotificationAsync(payload, new List<string> { notification.FirebaseToken });

                                    // Log the notification
                                    await db.ExecuteAsync(@"
                                    INSERT INTO NotificationLogs (ProductID, EmployeeID, NotificationMessage, SentDate) 
                                    VALUES (@ProductID, @EmployeeID, @Message, GETDATE())",
                                        new
                                        {
                                            notification.ProductID,
                                            notification.EmployeeID,
                                            Message = payload.Body
                                        });

                                    // Mark notification as processed
                                    await db.ExecuteAsync(
                                        "UPDATE LowStockNotifications SET IsProcessed = 1 WHERE ID = @ID",
                                        new { notification.ID }
                                    );

                                    LogInformation($"Sent notification for product {notification.ProductName} to employee {notification.EmployeeID}");
                                }
                                catch (Exception ex)
                                {
                                    LogError(ex, $"Failed to send notification for product {notification.ProductName}");
                                    // Continue with other notifications even if one fails
                                }
                            }
                            else
                            {
                                LogWarning($"No Firebase token for employee {notification.EmployeeID} - skipping notification");

                                // Mark as processed even though we couldn't send it
                                await db.ExecuteAsync(
                                    "UPDATE LowStockNotifications SET IsProcessed = 1 WHERE ID = @ID",
                                    new { notification.ID }
                                );
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, "Error processing notifications");
                        throw;
                    }
                }
            }

            LogInformation("Completed low stock notification check");
        }

        // Helper methods for logging (with fallback to Console if ILogger is not available)
        private void LogInformation(string message)
        {
            if (_logger != null)
                _logger.LogInformation(message);
            else
                Console.WriteLine($"INFO: {message}");
        }

        private void LogWarning(string message)
        {
            if (_logger != null)
                _logger.LogWarning(message);
            else
                Console.WriteLine($"WARNING: {message}");
        }

        private void LogError(Exception ex, string message)
        {
            if (_logger != null)
                _logger.LogError(ex, message);
            else
                Console.WriteLine($"ERROR: {message} - {ex.Message}");
        }
    }

}
