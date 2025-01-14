using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class OrderStage
{
    public Guid Id { get; set; }

    public string StageNameEn { get; set; } = null!;

    public string StageNameAr { get; set; } = null!;

    public string StageNameKr { get; set; } = null!;

    public string StageNameTr { get; set; } = null!;

    public string DescriptionEn { get; set; } = null!;

    public string DescriptionAr { get; set; } = null!;

    public string DescriptionKr { get; set; } = null!;

    public string DescriptionTr { get; set; } = null!;

    public int Sequence { get; set; }

    public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
}
