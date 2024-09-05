using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqCostumerRemainForReport
{
    public int IdEvent { get; set; }

    public string? EventId { get; set; }

    public string? Typeevent { get; set; }

    public string? Subb { get; set; }

    public DateTime? Datee { get; set; }

    public string? Costumer { get; set; }

    public decimal? Runningtotal { get; set; }
}
