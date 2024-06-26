using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class TrackType
{
    public Guid Id { get; set; }

    public string TypeAr { get; set; } = null!;

    public string? TypeEn { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
