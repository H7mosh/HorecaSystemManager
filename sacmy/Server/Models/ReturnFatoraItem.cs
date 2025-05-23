﻿using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ReturnFatoraItem
{
    public int Id { get; set; }

    public int ReId { get; set; }

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

    public decimal? Rub7Karton { get; set; }

    public string? Wajba { get; set; }

    public double? QttRemaining { get; set; }

    public string? Subb { get; set; }

    public string? CodIqd { get; set; }

    public string? BoxContain { get; set; }

    public decimal? PurchasePrise { get; set; }

    public string? Itemm { get; set; }
}
