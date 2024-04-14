using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class HorecaMenuImage
{
    public Guid Id { get; set; }

    public string Image { get; set; } = null!;

    public Guid HorecaInfoId { get; set; }

    public DateTime Createddate { get; set; }

    public virtual HorecaInformation HorecaInfo { get; set; } = null!;
}
