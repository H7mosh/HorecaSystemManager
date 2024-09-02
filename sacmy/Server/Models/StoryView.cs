using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class StoryView
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }

    public Guid StoryId { get; set; }

    public DateTime ViewedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Story Story { get; set; } = null!;
}
