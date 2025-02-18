using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sacmy.Server.Models
{
    [Table("StickyNotes")]
    public class StickyNote
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();  // Automatically generates GUID

        [Required]
        public string TableName { get; set; }  // E.g., "Products", "Orders"

        [Required]
        public Guid RecordId { get; set; }  // ID of the related record (foreign key)

        [Required]
        public Guid EmployeeId { get; set; }  // ID of the employee who created the note

        [Required]
        public string Note { get; set; }  // The note text

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Property (optional)
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
