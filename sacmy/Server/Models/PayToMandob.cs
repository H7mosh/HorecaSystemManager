﻿using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PayToMandob
{
    public int Id { get; set; }

    public string? Mandob { get; set; }

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

    public DateTime? Datee { get; set; }

    public string? ToOffice { get; set; }

    public string? Symbol { get; set; }
}
