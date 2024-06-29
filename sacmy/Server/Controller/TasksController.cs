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
        private readonly NotificationService _notificationService;

        public TasksController(SafeenCompanyDbContext context, FileService fileService, NotificationService notificationService)
        {
            _context = context;
            _fileService = fileService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] Guid UserId)
        {
            Employee employee = await _context.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == UserId);

            var tasks = await _context.Tasks
                .Include(e => e.AssignedToEmployeeNavigation)
                .Include(e => e.Status)
                .Select(e => new GetTaskViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Status = e.Status.StateEn,
                    StatusId = e.Status.Id,
                    AssignedToEmployee = e.AssignedToEmployeeNavigation.FirstName + " " + e.AssignedToEmployeeNavigation.LastName,
                    AssignedToEmployeeId = e.AssignedToEmployeeNavigation.Id,
                    EmployeeImage = "https://safinahmedcompany.com/assets/EmployeeImages/" + e.AssignedToEmployeeNavigation.Image,
                    CreatedDate = e.CreatedDate,
                    DeadlineDate = e.Deadline
                }).OrderByDescending(e => e.CreatedDate).ToListAsync();

            // Filter tasks based on UserId
            if (employee.Role.Role != "manager")
            {
                tasks = tasks.Where(t => t.AssignedToEmployeeId == UserId).ToList();
            }

            if (tasks == null || !tasks.Any())
            {
                return NotFound("There's No Tasks Yet");
            }

            return Ok(tasks);
           
        }

        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] PostTaskViewModel postTaskViewModel)
        {
            try
            {
                Employee employee = await _context.Employees.FindAsync(postTaskViewModel.AssignedToEmployee);
                sacmy.Server.Models.Task task = new Models.Task();

                task.Id = postTaskViewModel.Id;
                task.Title = postTaskViewModel.Title;
                task.Description = postTaskViewModel.Description;
                task.AssignedToEmployee = postTaskViewModel?.AssignedToEmployee;
                task.StatusId = postTaskViewModel.StatusId;
                task.Deadline = postTaskViewModel.Deadline;
                task.CreatedDate = DateTime.Now;
                task.IsDeleted = false;

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                await _notificationService.SendNotificationAsync("New Task Assigned", $"You have been assigned a new task: {task.Title}", [employee.FirebaseToken] , true);
                return Ok();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
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
                                CreatedBy = e.CreatedBy,
                                EmployeeName = e.CreatedByNavigation.FirstName + e.CreatedByNavigation.LastName,
                                EmployeeImage = "https://safinahmedcompany.com/assets/EmployeeImages/" + e.CreatedByNavigation.Image,
                                EmpolyeeRole = e.CreatedByNavigation.Role.Role,
                                CreatedDate = e.CreatedDate,
                            }).
                            OrderByDescending(e => e.CreatedDate).
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
                FileLink = string.IsNullOrEmpty(model.FileBase64) ? null : "https://safinahmedcompany.com/assets/TaskAttachment/" +model.FileName  ,
                CreatedBy = model.EmployeeId,
                CreatedDate = DateTime.Now,
                TaskId = model.TaskId
            };

            _context.Set<TaskNote>().Add(taskNote);
            await _context.SaveChangesAsync();

            return Ok(taskNote);
        }

        [HttpGet("GetTaskStatus")]
        public async Task<IActionResult> GetTaskStatus()
        {
            var status = await _context.Statuses.Select(e => new GetTaskStatus
                                {
                                    Id= e.Id,
                                    StatusAr = e.StateAr,
                                    StatusEn = e.StateEn,
                                }).ToListAsync();

            return Ok(status);
        }

        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody]UpdateTaskViewModel taskViewModel)
        {
            sacmy.Server.Models.Task task = await _context.Tasks.FindAsync(taskViewModel.Id);


            if (task != null)
            {
                task.StatusId = taskViewModel.StatusId;
                task.AssignedToEmployee = taskViewModel.AssignedToEmployeeId;
                task.Title = taskViewModel.Title;
                task.Description = taskViewModel.Description;
                task.DeletedDate = taskViewModel.DeadlineDate;
                taskViewModel.CreatedDate = taskViewModel.CreatedDate;

                await _context.SaveChangesAsync();

                return Ok(taskViewModel);
            }

            else
            {
                return NotFound("Cannot Find This Task");
            }
        }


    }
}
