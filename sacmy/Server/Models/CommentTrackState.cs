using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CommentTrackState
{
    public Guid Id { get; set; }

    public string? StateEn { get; set; }

    public string? StateAr { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? TrackCommentId { get; set; }

    public virtual TrackComment? TrackComment { get; set; }
}
