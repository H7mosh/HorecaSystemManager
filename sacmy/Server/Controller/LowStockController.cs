using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using sacmy.Server.Service;
using sacmy.Shared.ViewModels;
using sacmy.Shared.ViewModels.LowStockViewModels;
using System.Data;

namespace sacmy.Server.Controller
{
    [Route("api/lowstock")]
    [ApiController]
    public class LowStockController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IServiceProvider _serviceProvider;

        public LowStockController(IConfiguration config, IServiceProvider serviceProvider)
        {
            _connectionString = config.GetConnectionString("onlineConnectionString");
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetLowStockItems()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = @"
                SELECT 
                k.Id AS ProductID, 
                k.Name AS ProductName, 
                (k.Qtty_Purch - k.Qtty_Sales + k.Qtty_Return) AS Quantity, 
                e.EmployeeID, 
                e.Name, 
                e.FirebaseToken, 
                r.Threshold, 
                n.CreatedDate, 
                n.IsDeleted
                FROM KP_Store k
                JOIN LowStockRecipients r ON k.Id = r.ProductID
                JOIN Employees e ON r.EmployeeID = e.EmployeeID
                LEFT JOIN LowStockNotifications n ON k.Id = n.ProductID 
                    AND r.EmployeeID = n.EmployeeID
                WHERE (k.Qtty_Purch - k.Qtty_Sales + k.Qtty_Return) <= r.Threshold
                AND (n.IsDeleted = 0 OR n.IsDeleted IS NULL)";

                var lookup = new Dictionary<Guid, LowStockAlertViewModel>();

                var result = await db.QueryAsync<LowStockAlertViewModel, LowStockEmployeeViewModel, LowStockAlertViewModel>(
                    query,
                    (alert, employee) =>
                    {
                        if (!lookup.TryGetValue(alert.ProductID, out var lowStockAlert))
                        {
                            lowStockAlert = alert;
                            lowStockAlert.Recipients = new List<LowStockEmployeeViewModel>();
                            lookup.Add(alert.ProductID, lowStockAlert);
                        }
                        lowStockAlert.Recipients.Add(employee);
                        return lowStockAlert;
                    },
                    splitOn: "EmployeeID"
                );

                return Ok(lookup.Values);
            }
        }

        [HttpPost("monitor")]
        public async Task<IActionResult> AddMonitoredProduct([FromBody] MonitorProductViewModel model)
        {
            if (model == null || model.ProductID == Guid.Empty || model.EmployeeID == Guid.Empty || model.Threshold <= 0)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid input data. Product, employee, and threshold (greater than 0) are required."
                });
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {
                            // Check if the employee is already monitoring this product
                            var checkRecipientQuery = @"
                            SELECT ID FROM LowStockRecipients 
                            WHERE ProductID = @ProductID AND EmployeeID = @EmployeeID";

                            var existingRecipientId = await db.QueryFirstOrDefaultAsync<Guid?>(
                                checkRecipientQuery,
                                new { model.ProductID, model.EmployeeID },
                                transaction
                            );

                            if (existingRecipientId.HasValue)
                            {
                                // Update existing threshold
                                var updateQuery = @"
                                UPDATE LowStockRecipients 
                                SET Threshold = @Threshold 
                                WHERE ID = @ID";

                                await db.ExecuteAsync(
                                    updateQuery,
                                    new { ID = existingRecipientId.Value, model.Threshold },
                                    transaction
                                );

                                transaction.Commit();

                                return Ok(new ApiResponse
                                {
                                    Success = true,
                                    Message = $"Product monitoring threshold updated to {model.Threshold}."
                                });
                            }
                            else
                            {
                                // Insert new monitoring relationship
                                var insertRecipientQuery = @"
                                INSERT INTO LowStockRecipients (ID, ProductID, EmployeeID, Threshold, CreatedDate) 
                                VALUES (@ID, @ProductID, @EmployeeID, @Threshold, GETDATE())";

                                await db.ExecuteAsync(
                                    insertRecipientQuery,
                                    new
                                    {
                                        ID = Guid.NewGuid(),
                                        model.ProductID,
                                        model.EmployeeID,
                                        model.Threshold
                                    },
                                    transaction
                                );

                                transaction.Commit();

                                return Ok(new ApiResponse
                                {
                                    Success = true,
                                    Message = $"Product added to monitoring with threshold of {model.Threshold} units."
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return StatusCode(500, new ApiResponse
                            {
                                Success = false,
                                Message = $"Internal server error: {ex.Message}"
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ApiResponse
                    {
                        Success = false,
                        Message = $"Database connection failed: {ex.Message}"
                    });
                }
            }
        }

        [HttpPost("send-notifications")]
        public async Task<IActionResult> SendLowStockNotifications()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var notificationService = scope.ServiceProvider.GetRequiredService<LowStockNotificationService>();
                    await notificationService.CheckAndSendNotifications();
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Low stock notifications sent successfully!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"Error sending notifications: {ex.Message}"
                });
            }
        }
    }

}
