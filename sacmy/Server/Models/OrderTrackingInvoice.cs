using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sacmy.Server.Models;

[Table("OrderTrackingInvoice")]
public partial class OrderTrackingInvoice
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderTrackingId { get; set; }

    [Column("BuyFatoraId")]
    public int BuyFatoraId { get; set; }

    public bool IsPdfGenerated { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual OrderTracking OrderTracking { get; set; } = null!;

    public virtual BuyFatora BuyFatora { get; set; } = null!;
}
