using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PasabahceSeries
{
    public Guid Id { get; set; }

    public string Series { get; set; } = null!;

    public Guid HorecaInfoId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual HorecaInformation HorecaInfo { get; set; } = null!;
}
