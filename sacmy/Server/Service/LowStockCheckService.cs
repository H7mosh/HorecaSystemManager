using Microsoft.Data.SqlClient;
using System.Data;

namespace sacmy.Server.Service
{
    public class LowStockCheckerService : BackgroundService
    {
        private readonly string _connectionString;
        private readonly ILogger<LowStockCheckerService> _logger;

        public LowStockCheckerService(
            IConfiguration configuration,
            ILogger<LowStockCheckerService> logger = null)
        {
            _connectionString = configuration.GetConnectionString("onlineConnectionString");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await RunCheckLowStockProcedure();
                    LogInformation("CheckLowStock procedure executed successfully");
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error occurred while executing CheckLowStock procedure");
                }

                // Wait 5 minutes before the next execution
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task RunCheckLowStockProcedure()
        {
            LogInformation("Starting CheckLowStock procedure execution");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("[dbo].[CheckLowStock]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60; // Set a reasonable timeout (60 seconds)

                    await command.ExecuteNonQueryAsync();
                }
            }

            LogInformation("Completed CheckLowStock procedure execution");
        }

        // Helper methods for logging (with fallback to Console if ILogger is not available)
        private void LogInformation(string message)
        {
            if (_logger != null)
                _logger.LogInformation(message);
            else
                Console.WriteLine($"INFO: {message}");
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
