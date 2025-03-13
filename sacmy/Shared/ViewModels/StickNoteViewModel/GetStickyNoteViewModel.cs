using sacmy.Shared.ViewModels.EmployeeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.StickNoteViewModel
{
    public class GetStickyNoteViewModel
    {
        public Guid Id { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public GetEmployeeViewModel Employee { get; set; }
    }

}
