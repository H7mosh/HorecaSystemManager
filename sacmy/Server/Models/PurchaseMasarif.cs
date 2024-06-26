using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PurchaseMasarif
{
    public int Id { get; set; }

    public string? IxraciN { get; set; }

    public decimal? Gomrk { get; set; }

    public decimal? Amola { get; set; }

    public decimal? Ijra { get; set; }

    public decimal? Hamalya { get; set; }

    public decimal? Oxra { get; set; }

    public decimal? Tootaal { get; set; }

    public string? Notes { get; set; }

    public string? Subb { get; set; }

    public string? Uuser { get; set; }

    public string? EventId { get; set; }

    public DateOnly? Datee { get; set; }

    public bool? Runn { get; set; }
}
