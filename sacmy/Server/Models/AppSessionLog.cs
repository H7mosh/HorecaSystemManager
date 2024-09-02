using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class AppSessionLog
{
    public Guid SessionId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OpenTime { get; set; }

    public DateTime? CloseTime { get; set; }

    public string? OpenLocation { get; set; }

    public string? CloseLongitude { get; set; }

    public string? DeviceInfo { get; set; }

    public string? AppVersion { get; set; }

    public string? AdditionalInfo { get; set; }

    public virtual Customer? Customer { get; set; }
}
