using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.TasksViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        public TasksController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.
                Include(e => e.AssignedToEmployeeNavigation).
                Include(e => e.Status).
                Select(e => new GetTaskViewModel {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Status = e.Status.StateEn,
                AssignedToEmployee = e.AssignedToEmployeeNavigation.FirstName + e.AssignedToEmployeeNavigation.LastName,
                EmployeeImage = "https://safinahmedcompany.com/assets/EmployeeImages/"+e.AssignedToEmployeeNavigation.Image,
                CreatedDate = e.CreatedDate,
                DeadlineDate = e.Deadline
                }).ToListAsync();


            if (tasks is null)
            {
                return NotFound("There's No Tasks Yet");
            }

            return Ok(tasks);

        }

        [HttpGet("GetTaskNotes")]
        public async Task<IActionResult> GetTaskNotes([FromQuery] string taskId)
        {
            var taskNotes = await _context.TaskNotes.
                            Include(e => e.Task).
                            Include(e => e.CreatedByNavigation).
                            Where(e => e.TaskId == Guid.Parse(taskId)).
                            Select(e => new GetTaskNotes
                            { 
                                Id = e.Id,
                                Note = e.Note,
                                FileLink = e.FileLink,
                                EmployeeName = e.CreatedByNavigation.FirstName + e.CreatedByNavigation.LastName,
                                EmployeeImage = "https://safinahmedcompany.com/assets/EmployeeImages/" + e.CreatedByNavigation.Image,
                                CreatedDate = e.CreatedDate,
                            }).
                            ToListAsync();


            if (taskNotes is null)
            {
                return NotFound("There's No Tasks Yet");
            }

            return Ok(taskNotes);

        }
    }
}
