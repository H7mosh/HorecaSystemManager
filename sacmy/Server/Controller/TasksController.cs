using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.ViewModels.TasksViewModel;
using static System.Net.WebRequestMethods;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly FileService _fileService;
        private readonly SafeenCompanyDbContext _context;
        public TasksController(SafeenCompanyDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
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

        [HttpPost("PostTaskNote")]
        public async Task<IActionResult> PostTaskNote([FromBody] PostTaskNoteViewModel model, [FromQuery] string taskTitle)
        {
            Console.WriteLine("Received request to PostTaskNote");

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"ModelState Error for {key}: {error.ErrorMessage}");
                    }
                }
                return BadRequest(ModelState);
            }

            Console.WriteLine($"Received Note: {model.Note}");
            Console.WriteLine($"Received EmployeeId: {model.EmployeeId}");
            Console.WriteLine($"Received TaskId: {model.TaskId}");
            Console.WriteLine($"Received FileName: {model.FileName}");
            Console.WriteLine($"Received ContentType: {model.ContentType}");

            string fileLink = null;

            if (!string.IsNullOrEmpty(model.FileBase64))
            {
                Console.WriteLine($"Received File: {model.FileBase64.Length} characters");

                var fileBytes = Convert.FromBase64String(model.FileBase64);
                var filePath = Path.Combine("C:\\assets\\TaskAttachment", model.FileName);
                await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);
                fileLink = filePath;
            }

            var taskNote = new TaskNote
            {
                Id = Guid.NewGuid(),
                Note = model.Note,
                FileLink = "https://safinahmedcompany.com/assets/TaskAttachment/"+model.FileName,
                CreatedBy = model.EmployeeId,
                CreatedDate = DateTime.Now,
                TaskId = model.TaskId
            };

            _context.Set<TaskNote>().Add(taskNote);
            await _context.SaveChangesAsync();

            return Ok(taskNote);
        }



    }
}
