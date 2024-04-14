using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class OtherBrand
{
    public Guid Id { get; set; }

    public string? BrandName { get; set; }

    public Guid HorecaInfoId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual HorecaInformation HorecaInfo { get; set; } = null!;
}
