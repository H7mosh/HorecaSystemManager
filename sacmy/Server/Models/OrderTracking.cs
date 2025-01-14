using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sacmy.Server.Models;

public partial class OrderTracking
{
    [Key]
    public Guid Id { get; set; }

    public int? OrderId { get; set; }

    public Guid StageId { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    [ForeignKey("OrderId")]
    public virtual OnlineOrder? Order { get; set; }

    public virtual ICollection<OrderTrackingAttachment> OrderTrackingAttachments { get; set; } = new List<OrderTrackingAttachment>();

    public virtual ICollection<OrderTrackingInvoice> OrderTrackingInvoices { get; set; } = new List<OrderTrackingInvoice>();

    [ForeignKey("StageId")]
    public virtual OrderStage Stage { get; set; } = null!;
}
