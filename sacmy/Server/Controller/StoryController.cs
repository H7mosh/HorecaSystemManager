﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Server.Service;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.StoryViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;

        public StoryController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetStoryViewModel>>>> GetStories()
        {
            var currentDateTime = DateTime.Now;

            var stories = await _context.Stories
                .Include(s => s.CreatedByNavigation)
                .Where(s => !s.IsDeleted)
                .Select(s => new GetStoryViewModel
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    MediaUrl = s.MediaUrl,
                    MediaType = s.MediaType,
                    Description = s.Description,
                    Message = s.Message,
                    CreatedBy = s.CreatedBy,
                    BrandName = s.CreatedByNavigation.NameAr,
                    BrandImage = s.CreatedByNavigation.Image,
                    CreatedAt = s.CreatedAt,
                    Expiration = s.Expiration,
                    ViewCount = s.StoryViews.Count
                })
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return Ok(new ApiResponse<IEnumerable<GetStoryViewModel>>
            {
                Success = true,
                Message = "Stories retrieved successfully",
                Data = stories,
                TotalCount = stories.Count
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetStoryViewModel>>> GetStory(Guid id)
        {
            var story = await _context.Stories
                .Include(s => s.CreatedByNavigation)
                .Include(s => s.StoryViews)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (story == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Story not found"
                });
            }

            var viewModel = new GetStoryViewModel
            {
                Id = story.Id,
                UserId = story.UserId,
                MediaUrl = story.MediaUrl,
                MediaType = story.MediaType,
                Description = story.Description,
                Message = story.Message,
                CreatedBy = story.CreatedBy,
                BrandName = story.CreatedByNavigation.NameAr,
                CreatedAt = story.CreatedAt,
                Expiration = story.Expiration,
                ViewCount = story.StoryViews.Count
            };

            return Ok(new ApiResponse<GetStoryViewModel>
            {
                Success = true,
                Message = "Story retrieved successfully",
                Data = viewModel
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<GetStoryViewModel>>> CreateStory(CreateStoryViewModel request)
        {
            var brandExists = await _context.Brands.AnyAsync(b => b.Id == request.CreatedBy);

            if (!brandExists)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "The specified brand does not exist."
                });
            }

            var creationTime = DateTime.Now;

            var expirationTime = creationTime.AddHours(24);

            var story = new Story
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                MediaUrl = request.MediaUrl,
                MediaType = request.MediaType,
                Description = request.Description,
                Message = request.Message,
                CreatedBy = request.CreatedBy,
                CreatedAt = creationTime,
                Expiration = expirationTime,
                IsDeleted = false
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            var brand = await _context.Brands.FindAsync(request.CreatedBy);

            var viewModel = new GetStoryViewModel
            {
                Id = story.Id,
                UserId = story.UserId,
                MediaUrl = story.MediaUrl,
                MediaType = story.MediaType,
                Description = story.Description,
                Message = story.Message,
                CreatedBy = story.CreatedBy,
                BrandName = brand?.NameAr,
                CreatedAt = story.CreatedAt,
                Expiration = story.Expiration,
                ViewCount = 0
            };

            return Ok(new ApiResponse<GetStoryViewModel>
            {
                Success = true,
                Message = "Story created successfully",
                Data = viewModel
            });
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateStory(Guid id, UpdateStoryViewModel request)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null || story.IsDeleted)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Story not found"
                });
            }

            // Validate if the brand exists if it's being updated
            if (request.CreatedBy != story.CreatedBy)
            {
                var brandExists = await _context.Brands.AnyAsync(b => b.Id == request.CreatedBy);
                if (!brandExists)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "The specified brand does not exist."
                    });
                }
            }

            // Update story properties
            story.MediaUrl = request.MediaUrl ?? story.MediaUrl;
            story.MediaType = request.MediaType ?? story.MediaType;
            story.Description = request.Description ?? story.Description;
            story.Message = request.Message ?? story.Message;
            story.CreatedBy = request.CreatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryExists(id))
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Story not found"
                    });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Story updated successfully"
            });
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteStory(Guid id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null || story.IsDeleted)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Story not found"
                });
            }

            story.IsDeleted = true;
            story.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Story deleted successfully"
            });
        }

        [HttpPost("UploadMedia")]
        public async Task<ActionResult<ApiResponse>> UploadMedia([FromForm] IFormFile media, [FromForm] string mediaType)
        {
            try
            {
                if (media == null || media.Length == 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "No media file provided."
                    });
                }

                // Validate media type
                if (string.IsNullOrEmpty(mediaType) || (mediaType != "image" && mediaType != "video"))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Invalid media type. Only 'image' or 'video' are allowed."
                    });
                }

                // Use the FileService to upload the media
                var fileService = new FileService();
                string targetPath = @"C:\assets\Stories";
                string fileName = await fileService.UploadFileAsync(media, $"Story-{Guid.NewGuid()}", targetPath);

                // Create the media URL
                var mediaUrl = $"https://api.safinahmedtech.com/assets/Stories/{fileName}";

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Media uploaded successfully",
                    Data = mediaUrl
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while uploading the media: {ex.Message}"
                });
            }
        }

        [HttpPost("AddView")]
        public async Task<ActionResult<ApiResponse>> AddStoryView(AddStoryViewModel request)
        {
            var story = await _context.Stories.FindAsync(request.StoryId);
            if (story == null || story.IsDeleted || story.Expiration < DateTime.Now)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Story not found or has expired"
                });
            }

            // Check if customer exists
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == request.CustomerId);
            if (!customerExists)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "The specified customer does not exist."
                });
            }

            // Check if this customer has already viewed this story
            var existingView = await _context.StoryViews
                .FirstOrDefaultAsync(v => v.StoryId == request.StoryId && v.CustomerId == request.CustomerId);

            if (existingView != null)
            {
                // Update the viewed time
                existingView.ViewedAt = DateTime.Now;
            }
            else
            {
                // Create a new view record
                var storyView = new StoryView
                {
                    Id = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    StoryId = request.StoryId,
                    ViewedAt = DateTime.Now
                };

                _context.StoryViews.Add(storyView);
            }

            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Story view recorded successfully"
            });
        }

        private bool StoryExists(Guid id)
        {
            return _context.Stories.Any(e => e.Id == id && !e.IsDeleted);
        }
    }
}
