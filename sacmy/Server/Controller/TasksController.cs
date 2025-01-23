using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Client.Pages.Invoice;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.ViewModels.Notification;
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
        private readonly ILogger<NotificationService> _notificationLogger;

        public TasksController(SafeenCompanyDbContext context, FileService fileService, IConfiguration configuration , ILogger<NotificationService> notificationLogger)
        {
            _context = context;
            _fileService = fileService;
            _notificationLogger = notificationLogger;
            _notificationService = new NotificationService(configuration, "SafinAhmedManagerNotificationKeys" , _notificationLogger); // Use manager notifications key for employee tasks
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] Guid UserId)
        {
            Employee employee = await _context.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == UserId);

            var tasks = await _context.Tasks
                .Include(e => e.AssignedToEmployeeNavigation)
                .Include(e => e.Type)
                .Include(e => e.Status)
                .Select(e => new GetTaskViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    TypeEn = e.Type.TypeEn,
                    TypeAr = e.Type.TypeAr,
                    TypeTr = e.Type.TypeTr,
                    StatusEn = e.Status.StateEn,
                    StatusAr = e.Status.StateAr,
                    StatusTr = e.Status.StateEn,
                    StatusId = e.Status.Id,
                    InvoiceId = e.InvoiceId,
                    CutsomerName = e.Customer.Customer1,
                    CutsomerId = e.CustomerId,
                    AssignedToEmployee = e.AssignedToEmployeeNavigation.FirstName + " " + e.AssignedToEmployeeNavigation.LastName,
                    AssignedToEmployeeId = e.AssignedToEmployeeNavigation.Id,
                    EmployeeId = e.AssignedToEmployeeNavigation.Id.ToString(),
                    EmployeeImage = e.AssignedToEmployeeNavigation.Image,
                    EmployeeFirebaseToken = e.AssignedToEmployeeNavigation.FirebaseToken,
                    CreatedBy = e.CreatedBy ?? Guid.NewGuid(),
                    CreatedbyName = e.CreatedByNavigation.FirstName + " " + e.CreatedByNavigation.LastName,
                    CreatedbyImage = e.CreatedByNavigation.Image,
                    CreatedDate = (DateTime)e.CreatedDate,
                    DeadlineDate = e.Deadline
                }).OrderByDescending(e => e.CreatedDate).ToListAsync();

            // Filter tasks based on UserId
            if (employee.Role.Role != "manager")
            {
                tasks = tasks.Where(t => t.AssignedToEmployeeId == UserId || t.CreatedBy == UserId).ToList();
            }

            if (tasks == null || !tasks.Any())
            {
                return Ok(new List<GetTaskViewModel>());
            }

            return Ok(tasks);
        }

        [HttpGet("GetTasksByInvoiceIdOrCustomerId")]
        public async Task<IActionResult> GetTasksByInvoiceIdOrCustomerId([FromQuery] Guid UserId, [FromQuery] int? invoiceId, [FromQuery] int? customerId)
        {
            Employee employee = await _context.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == UserId);
            var tasks = new List<GetTaskViewModel>();

            if (invoiceId.HasValue)
            {
                tasks = await _context.Tasks
               .Include(e => e.AssignedToEmployeeNavigation)
               .Include(e => e.Type)
               .Include(e => e.Status)
               .Where(e => e.InvoiceId == invoiceId)
               .Select(e => new GetTaskViewModel
               {
                   Id = e.Id,
                   Title = e.Title,
                   Description = e.Description,
                   TypeEn = e.Type.TypeEn,
                   TypeAr = e.Type.TypeAr,
                   TypeTr = e.Type.TypeTr,
                   StatusEn = e.Status.StateEn,
                   StatusAr = e.Status.StateAr,
                   StatusTr = e.Status.StateEn,
                   StatusId = e.Status.Id,
                   InvoiceId = e.InvoiceId,
                   CutsomerName = e.Customer.Customer1,
                   AssignedToEmployee = e.AssignedToEmployeeNavigation.FirstName + " " + e.AssignedToEmployeeNavigation.LastName,
                   AssignedToEmployeeId = e.AssignedToEmployeeNavigation.Id,
                   EmployeeImage = "https://api.safinahmedtech.com/assets/EmployeeImages/" + e.AssignedToEmployeeNavigation.Image,
                   EmployeeFirebaseToken = e.AssignedToEmployeeNavigation.FirebaseToken,
                   CreatedBy = e.CreatedBy ?? Guid.NewGuid(),
                   CreatedbyName = e.CreatedByNavigation.FirstName + " " + e.CreatedByNavigation.LastName,
                   CreatedbyImage = "https://api.safinahmedtech.com/assets/EmployeeImages/" + e.CreatedByNavigation.Image,
                   CreatedDate = (DateTime)e.CreatedDate,
                   DeadlineDate = e.Deadline
               }).OrderByDescending(e => e.CreatedDate).ToListAsync();

                // Filter tasks based on UserId
                if (employee.Role.Role != "manager")
                {
                    tasks = tasks.Where(t => t.AssignedToEmployeeId == UserId).ToList();
                }

                if (tasks == null || !tasks.Any())
                {
                    return Ok(new List<GetTaskViewModel>());
                }

                return Ok(tasks);
            }

            else if (customerId.HasValue)
            {
                tasks = await _context.Tasks
                .Include(e => e.AssignedToEmployeeNavigation)
                .Include(e => e.Type)
                .Include(e => e.Status)
                .Where(e => e.CustomerId == customerId)
                .Select(e => new GetTaskViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    TypeEn = e.Type.TypeEn,
                    TypeAr = e.Type.TypeAr,
                    TypeTr = e.Type.TypeTr,
                    StatusEn = e.Status.StateEn,
                    StatusAr = e.Status.StateAr,
                    StatusTr = e.Status.StateEn,
                    StatusId = e.Status.Id,
                    InvoiceId = e.InvoiceId,
                    CutsomerName = e.Customer.Customer1,
                    AssignedToEmployee = e.AssignedToEmployeeNavigation.FirstName + " " + e.AssignedToEmployeeNavigation.LastName,
                    AssignedToEmployeeId = e.AssignedToEmployeeNavigation.Id,
                    EmployeeImage = "https://api.safinahmedtech.com/assets/EmployeeImages/" + e.AssignedToEmployeeNavigation.Image,
                    EmployeeFirebaseToken = e.AssignedToEmployeeNavigation.FirebaseToken,
                    CreatedBy = e.CreatedBy ?? Guid.NewGuid(),
                    CreatedbyName = e.CreatedByNavigation.FirstName + " " + e.CreatedByNavigation.LastName,
                    CreatedbyImage = "https://api.safinahmedtech.com/assets/EmployeeImages/" + e.CreatedByNavigation.Image,
                    CreatedDate = (DateTime)e.CreatedDate,
                    DeadlineDate = e.Deadline
                }).OrderByDescending(e => e.CreatedDate).ToListAsync();

                // Filter tasks based on UserId
                if (employee.Role.Role != "manager")
                {
                    tasks = tasks.Where(t => t.AssignedToEmployeeId == UserId).ToList();
                }

                if (tasks == null || !tasks.Any())
                {
                    return Ok(new List<GetTaskViewModel>());
                }

                return Ok(tasks);
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
                task.TypeId = postTaskViewModel.TaskTypeId;
                task.Title = postTaskViewModel.Title;
                task.Description = postTaskViewModel.Description;
                task.CustomerId = postTaskViewModel.CustomerId;
                task.InvoiceId = postTaskViewModel.InvoiceId;
                task.AssignedToEmployee = postTaskViewModel?.AssignedToEmployee;
                task.CreatedBy = postTaskViewModel?.CreatedBy;
                task.StatusId = postTaskViewModel.StatusId;
                task.Deadline = postTaskViewModel.Deadline;
                task.CreatedDate = DateTime.Now;
                task.IsDeleted = false;

                NotificationPayload notificationPayload = new NotificationPayload
                {
                    Title = "New Task Assigned",
                    Body = $"You have been assigned a new task: {task.Title}",
                    Type = "task",
                    IsEmployeeNotification = true ,
                };

                


                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                await _notificationService.SendNotificationAsync(notificationPayload, [employee.FirebaseToken]);
                return Ok();
            }
            catch (Exception ex)
            {
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
                                FileLink = e.FilelInk,
                                CreatedBy = e.CreatedBy,
                                EmployeeName = e.CreatedByNavigation.FirstName + e.CreatedByNavigation.LastName,
                                EmployeeImage = e.CreatedByNavigation.Image,
                                EmpolyeeFirebaseToken = e.CreatedByNavigation.FirebaseToken,
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

                try
                {
                    var fileBytes = Convert.FromBase64String(model.FileBase64);
                    var uploadDirectory = @"C:\assets\TaskAttachment";

                    
                    var fileName = await _fileService.UploadFileAsync(
                        new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", model.FileName),
                        taskTitle,
                        uploadDirectory
                    );

                    fileLink = $"https://api.safinahmedtech.com/assets/TaskAttachment/{fileName}";
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error saving file: {ex.Message}");
                }
            }

            var taskNote = new TaskNote
            {
                Id = Guid.NewGuid(),
                Note = model.Note,
                FilelInk = fileLink,
                CreatedBy = model.EmployeeId,
                CreatedDate = DateTime.Now,
                TaskId = model.TaskId
            };


            NotificationPayload notificationPayload = new NotificationPayload
            {
                Title = "New Comment Added",
                Body = $"{model.Note}",
                Type = "comment",
                IsEmployeeNotification = true,
            };

            _context.Set<TaskNote>().Add(taskNote);
            await _notificationService.SendNotificationAsync(notificationPayload, [model.Employeefirebasetoken]);
            await _context.SaveChangesAsync();

            return Ok(taskNote);
        }

        [HttpGet("GetTaskStatus")]
        public async Task<IActionResult> GetTaskStatus()
        {
            var status = await _context.Statuses.Select(e => new GetTaskStatus
                                {
                                    Id = e.Id,
                                    StatusAr = e.StateAr,
                                    StatusEn = e.StateEn,
                                }).ToListAsync();

            return Ok(status);
        }

        [HttpGet("GetTaskType")]
        public async Task<IActionResult> GetTaskType()
        {
            var taskTypes = await _context.TaskTypes.OrderByDescending(e => e.CreatedDate)
                            .Select(e => new GetTaskType
                            {
                                Id = e.Id,
                                TypeAr = e.TypeAr,
                                TypeEn = e.TypeEn,
                                TypeTr = e.TypeKr,
                                TypeKr = e.TypeKr,
                            }).ToListAsync();

            return Ok(taskTypes);
        }

        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskViewModel taskViewModel)
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
