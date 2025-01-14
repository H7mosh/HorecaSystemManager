using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationJobStatus : ControllerBase
    {
        [HttpGet("job-status/{jobId}")]
        public IActionResult GetJobStatus(string jobId)
        {
            try
            {
                var connection = JobStorage.Current.GetConnection();
                var job = connection.GetJobData(jobId);

                if (job == null)
                {
                    return NotFound($"No job found with ID: {jobId}");
                }

                var status = new
                {
                    JobId = jobId,
                    State = job.State,
                    CreatedAt = job.CreatedAt,
                    ScheduledAt = connection.GetJobParameter(jobId, "ScheduledAt"),
                    LastExecution = connection.GetJobParameter(jobId, "LastExecution"),
                };

                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to get job status: {ex.Message}");
            }
        }
    }
}
