﻿using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqPayFromCostomerReport
{
    public int Id { get; set; }

    public string? Customer { get; set; }

    public string? Address { get; set; }

    public decimal? Total { get; set; }

    public string? Notes { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? PayType { get; set; }

    public string? Subb { get; set; }

    public string? EventId { get; set; }

    public bool? Runn { get; set; }

    public DateOnly? Datee { get; set; }

    public string? ToOffice { get; set; }

    public decimal? LastRemain { get; set; }

    public decimal? Safiremain { get; set; }

    public decimal? Dolar { get; set; }

    public decimal? Iqd { get; set; }
}
