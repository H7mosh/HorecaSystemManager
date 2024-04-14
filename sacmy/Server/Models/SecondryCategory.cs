using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class SecondryCategory
{
    public Guid Id { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? NameTr { get; set; }

    public string? NameKr { get; set; }

    public virtual ICollection<Item1> Item1s { get; set; } = new List<Item1>();

    public virtual ICollection<OnlineOrderItem> OnlineOrderItems { get; set; } = new List<OnlineOrderItem>();
}
