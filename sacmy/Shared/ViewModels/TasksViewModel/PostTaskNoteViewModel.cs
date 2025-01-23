using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace sacmy.Shared.ViewModels.TasksViewModel
{
    public class PostTaskNoteViewModel
    {
        public string Note { get; set; } = null!;
        public string? FileBase64 { get; set; } // Base64 encoded file
        public string? FileName { get; set; } // For sending the file name to the server
        public string? ContentType { get; set; } // For sending the content type to the server
        public Guid EmployeeId { get; set; }
        public string Employeefirebasetoken { get; set; }
        public Guid TaskId { get; set; }
    }
}
