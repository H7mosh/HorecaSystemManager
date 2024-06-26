using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Specification
{
    public Guid Id { get; set; }

    public string? TitelAr { get; set; }

    public string? TitelEn { get; set; }

    public string? TitleKr { get; set; }

    public string? TitleTr { get; set; }

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? DescriptionTr { get; set; }

    public string? DescriptionKr { get; set; }
}
