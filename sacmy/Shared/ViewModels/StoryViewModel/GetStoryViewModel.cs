using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.StoryViewModel
{
    public class GetStoryViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public Guid CreatedBy { get; set; }
        public string BrandName { get; set; }
        public string BrandImage {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Expiration { get; set; }
        public int ViewCount { get; set; }
        public bool IsExpired => DateTime.Now > Expiration;
        public string TimeRemaining => GetTimeRemaining();

        private string GetTimeRemaining()
        {
            if (IsExpired)
                return "Expired";

            var remaining = Expiration - DateTime.Now;

            if (remaining.TotalHours > 1)
                return $"{Math.Floor(remaining.TotalHours)}h {remaining.Minutes}m";
            else if (remaining.TotalMinutes > 1)
                return $"{remaining.Minutes}m {remaining.Seconds}s";
            else
                return $"{remaining.Seconds}s";
        }
    }
}
