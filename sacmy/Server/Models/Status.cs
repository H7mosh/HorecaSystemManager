using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Status
{
    public Guid Id { get; set; }

    public string? StateAr { get; set; }

    public string? StateEn { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<TrackCommentState> TrackCommentStates { get; set; } = new List<TrackCommentState>();
}
