using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Story
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string MediaUrl { get; set; } = null!;

    public string? MediaType { get; set; }

    public string? Description { get; set; }

    public string? Message { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime Expiration { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Brand CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();
}
