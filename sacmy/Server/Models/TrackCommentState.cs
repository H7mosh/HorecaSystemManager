using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class TrackCommentState
{
    public Guid Id { get; set; }

    public Guid? TrackCommentId { get; set; }

    public Guid? StateId { get; set; }

    public virtual Status? State { get; set; }

    public virtual TrackComment? TrackComment { get; set; }
}
