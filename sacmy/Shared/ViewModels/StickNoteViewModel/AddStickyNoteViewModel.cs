using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.StickNoteViewModel
{
    public class AddStickyNoteViewModel
    {
        [Required]
        public string TableName { get; set; }

        [Required]
        public Guid RecordId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public string Note { get; set; }
    }
}
