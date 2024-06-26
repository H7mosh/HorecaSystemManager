
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.UserViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Code { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? JobTitle { get; set; }

        public string? Branch { get; set; }

        public string? Brand { get; set; }

        public string? Image { get; set; }

        public string? FirebaseToken { get; set; }

        public string? Role { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
