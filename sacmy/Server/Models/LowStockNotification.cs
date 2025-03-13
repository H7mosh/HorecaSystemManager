using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sacmy.Server.Models
{
    public class LowStockNotification
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid(); // Unique Identifier

        [Required]
        public Guid ProductID { get; set; } // Product Reference

        [Required]
        public Guid EmployeeID { get; set; } // Employee Reference

        [Required]
        public bool IsProcessed { get; set; } = false; // Whether notification has been sent

        [Required]
        public bool IsDeleted { get; set; } = false; // Soft delete flag

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; set; } // Nullable: Stores the deletion time

        public int StockLevelAtCreation { get; set; } // Stock level when notification was created (optional)

        // Navigation properties
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; } // Navigation to Employee

        [NotMapped] // Prevents EF from trying to map KpStore
        public KpStore Product { get; set; } // Must be fetched manually
    }
}
