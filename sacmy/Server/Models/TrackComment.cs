using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class TrackComment
{
    public Guid Id { get; set; }

    public Guid? TrackId { get; set; }

    public Guid? EmployeeId { get; set; }

    public string? Comment { get; set; }

    public Guid? AssignedTo { get; set; }

    public DateTime? ReOpenAt { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Employee? AssignedToNavigation { get; set; }

    public virtual ICollection<CommentTrackState> CommentTrackStates { get; set; } = new List<CommentTrackState>();

    public virtual Employee? Employee { get; set; }

    public virtual Track? Track { get; set; }

    public virtual ICollection<TrackCommentState> TrackCommentStates { get; set; } = new List<TrackCommentState>();
}
