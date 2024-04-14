using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class EventDeleted
{
    public int Id { get; set; }

    public string? Typeevent { get; set; }

    public string? TypeeventId { get; set; }

    public string? TypeeventName { get; set; }

    public DateTime? TypeeventDate { get; set; }

    public string? UserDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public decimal? EventTotal { get; set; }

    public string? Subb { get; set; }
}
