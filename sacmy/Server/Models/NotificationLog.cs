using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sacmy.Server.Models
{
    public class NotificationLog
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid(); // Unique Identifier

        [Required]
        public Guid ProductID { get; set; } // Reference to Product

        [Required]
        public Guid EmployeeID { get; set; } // Reference to Employee

        [Required]
        [MaxLength(500)]
        public string NotificationMessage { get; set; } // Notification Content

        public DateTime SentDate { get; set; } = DateTime.UtcNow; // Date the notification was sent

        // ✅ Navigation Property for Employee (Valid)
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        // ❌ Remove invalid navigation to KpStore
        [NotMapped] // ✅ Prevents EF from mapping this field
        public KpStore Product { get; set; } // Must be fetched manually via SQL
    }
}
