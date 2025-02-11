using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TasksViewModel
{
    public class GetTaskViewModel
    {
        public Guid Id {  get; set; }
        public string Title { get; set; }
        public string TypeEn { get; set; }
        public string TypeTr { get; set; }
        public string TypeAr { get; set; }
        public string Description { get; set; }
        public string StatusEn { get; set; }
        public string StatusTr { get; set; }
        public string StatusAr { get; set; }
        public Guid StatusId {  get; set; }
        public string? CutsomerName { get; set; }
        public int? CutsomerId{ get; set; }
        public int? InvoiceId { get; set; }
        public string AssignedToEmployee { get; set; }
        public Guid AssignedToEmployeeId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeFirebaseToken { get; set; }
        public string EmployeeImage { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedbyName {  get; set; }
        public string CreatedbyImage {  get; set; }
        public string CreatedbyFirebaseToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
