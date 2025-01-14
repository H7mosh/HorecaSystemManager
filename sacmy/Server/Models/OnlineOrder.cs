using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class OnlineOrder
{
    public int OrderId { get; set; }

    public DateTimeOffset? Date { get; set; }

    public int? CustomerId { get; set; }

    public string? CostumerName { get; set; }

    public string? Sub { get; set; }

    public string? Note { get; set; }

    public bool? Checked { get; set; }

    public DateTimeOffset? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? Address { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OnlineOrderItem> OnlineOrderItems { get; set; } = new List<OnlineOrderItem>();

    public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
}
