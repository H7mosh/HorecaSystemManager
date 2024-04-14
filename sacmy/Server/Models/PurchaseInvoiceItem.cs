using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PurchaseInvoiceItem
{
    public int Id { get; set; }

    public int PuId { get; set; }

    public int? Secode { get; set; }

    public string? Codd { get; set; }

    public string? Typee { get; set; }

    public string? Factoryy { get; set; }

    /// <summary>
    /// Qtt_Remaining
    /// </summary>
    public double? Quantity { get; set; }

    public float? Weznn { get; set; }

    public string? QiyasUnit { get; set; }

    public double? Countt { get; set; }

    public decimal? Prise { get; set; }

    public decimal? Total { get; set; }

    public string? Wajba { get; set; }

    public string? Subb { get; set; }

    public string? CodIqd { get; set; }

    public string? BoxContain { get; set; }

    public decimal? Masraf { get; set; }

    public decimal? TotlPrise { get; set; }

    public string? Itemm { get; set; }

    /// <summary>
    /// Qtt_Remaining
    /// </summary>
    public double? FirstQtty { get; set; }

    /// <summary>
    /// Qtt_Remaining
    /// </summary>
    public double? Ziyada { get; set; }

    /// <summary>
    /// Qtt_Remaining
    /// </summary>
    public double? Naqis { get; set; }
}
