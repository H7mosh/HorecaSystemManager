﻿using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PayToOffice
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Offiice { get; set; }

    public string? Address { get; set; }

    public decimal? Ptotal { get; set; }

    public decimal? Dolar { get; set; }

    public decimal? IqdTtal { get; set; }

    public string? Note { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? Subb { get; set; }

    public string? EventId { get; set; }

    public string? Symbol { get; set; }
}
