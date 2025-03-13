using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sacmy.Server.Models
{
    public class LowStockRecipient
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid(); // Unique Identifier

        [Required]
        public Guid ProductID { get; set; } // Product Reference

        [Required]
        public Guid EmployeeID { get; set; } // Employee Reference

        [Required]
        public int Threshold { get; set; } // Notification threshold - alert when stock level drops to this value or below

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; } // Navigation to Employee

        [NotMapped] // Prevents EF from trying to map KpStore
        public KpStore Product { get; set; } // Must be fetched manually
    }
}
