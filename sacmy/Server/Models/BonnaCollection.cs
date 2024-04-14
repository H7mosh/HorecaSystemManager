using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class BonnaCollection
{
    public Guid Id { get; set; }

    public string BonnaCollection1 { get; set; } = null!;

    public Guid HorecaInfoId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual HorecaInformation HorecaInfo { get; set; } = null!;
}
