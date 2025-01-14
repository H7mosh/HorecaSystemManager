using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public class OrderTrackingAttachment
{
    public Guid Id { get; set; }
    public Guid OrderTrackingId { get; set; }
    public string Attachment { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual OrderTracking OrderTracking { get; set; }
}
