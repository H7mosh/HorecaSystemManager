using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class TaskNote
{
    public Guid Id { get; set; }

    public string Note { get; set; } = null!;

    public string? FileLink { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid TaskId { get; set; }

    public virtual Employee CreatedByNavigation { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
