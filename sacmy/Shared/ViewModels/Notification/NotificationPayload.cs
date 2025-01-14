using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Notification
{
    public class NotificationPayload
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Sound { get; set; } = "reminder.wav";
        public bool IsEmployeeNotification { get; set; }
    }
}
