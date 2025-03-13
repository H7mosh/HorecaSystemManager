using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.StickNoteViewModel;
using System;

namespace sacmy.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StickyNotesController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;

        public StickyNotesController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GetStickyNoteViewModel>>>> GetByRecord([FromQuery] string tableName,[FromQuery] string recordId)
        {
            try
            {
                var stickyNotes = await _context.StickyNotes
                    .Where(n => n.TableName == tableName && n.RecordId == recordId)
                    .Include(n => n.Employee) // Assuming you have Employee navigation property
                    .OrderByDescending(n => n.CreatedDate)
                    .Select(note => new GetStickyNoteViewModel
                    {
                        Id = note.Id,
                        TableName = note.TableName,
                        RecordId = note.RecordId,
                        EmployeeId = note.EmployeeId,
                        Note = note.Note,
                        CreatedDate = note.CreatedDate,
                        Employee = new GetEmployeeViewModel
                        {
                            Id = note.Employee.Id,
                            FirstName = note.Employee.FirstName,
                            LastName = note.Employee.LastName,
                            Image = note.Employee.Image,
                            Branch = note.Employee.Branch,
                            FirebaseToken = note.Employee.FirebaseToken,
                            JobTitle = note.Employee.JobTitle,
                        }
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<GetStickyNoteViewModel>>
                {
                    Success = true,
                    Message = "Sticky notes retrieved successfully.",
                    Data = stickyNotes
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while retrieving sticky notes.",
                    Data = ex.Message
                });
            }
        }

        // POST: api/StickyNotes
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GetStickyNoteViewModel>>> Create([FromBody] AddStickyNoteViewModel model)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid input",
                    Data = ModelState
                });
            }

            // Create entity
            var entity = new StickyNote
            {
                TableName = model.TableName,
                RecordId = model.RecordId,
                EmployeeId = model.EmployeeId,
                Note = model.Note,
                CreatedDate = DateTime.UtcNow
            };

            _context.StickyNotes.Add(entity);
            await _context.SaveChangesAsync();

            var noteVM = new GetStickyNoteViewModel
            {
                Id = entity.Id,
                TableName = entity.TableName,
                RecordId = entity.RecordId,
                EmployeeId = entity.EmployeeId,
                Note = entity.Note,
                CreatedDate = entity.CreatedDate
            };

            var response = new ApiResponse<GetStickyNoteViewModel>
            {
                Success = true,
                Message = "Sticky note created successfully.",
                Data = noteVM
            };

            // Return 201 Created with the resource
            return CreatedAtAction(
                nameof(GetById),
                new { id = entity.Id },
                response
            );
        }

        // GET: api/StickyNotes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetStickyNoteViewModel>>> GetById(Guid id)
        {
            var stickyNote = await _context.StickyNotes.FindAsync(id);

            if (stickyNote == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = $"Sticky note with ID {id} not found.",
                    Data = null
                });
            }

            var noteVM = new GetStickyNoteViewModel
            {
                Id = stickyNote.Id,
                TableName = stickyNote.TableName,
                RecordId = stickyNote.RecordId,
                EmployeeId = stickyNote.EmployeeId,
                Note = stickyNote.Note,
                CreatedDate = stickyNote.CreatedDate
            };

            var response = new ApiResponse<GetStickyNoteViewModel>
            {
                Success = true,
                Message = "Sticky note retrieved successfully.",
                Data = noteVM
            };

            return Ok(response);
        }
    }
}
