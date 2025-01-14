using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Notification
{
    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public List<UserIdAndToken> UserIdAndTokenList { get; set; }
        public DateTime? ScheduleTime { get; set; }
    }
}
