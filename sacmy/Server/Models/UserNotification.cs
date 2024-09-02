using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class UserNotification
{
    public Guid Id { get; set; }

    public Guid NotificationId { get; set; }

    public int CustomerId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Notification Notification { get; set; } = null!;
}
