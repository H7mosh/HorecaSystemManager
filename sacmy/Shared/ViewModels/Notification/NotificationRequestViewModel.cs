using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Notification
{
    public class NotificationRequestViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> FirebaseTokens { get; set; }
        public string? ImageBase64 { get; set; }
        public bool employeeNotification { get; set; }
    }
}
