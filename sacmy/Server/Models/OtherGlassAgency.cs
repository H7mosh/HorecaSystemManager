using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class OtherGlassAgency
{
    public Guid Id { get; set; }

    public string? GlassAgencyName { get; set; }

    public Guid HorecaInfoId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual HorecaInformation HorecaInfo { get; set; } = null!;
}
