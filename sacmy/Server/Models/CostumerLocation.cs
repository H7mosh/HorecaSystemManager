using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CostumerLocation
{
    public int Id { get; set; }

    public string? Location { get; set; }

    public string? City { get; set; }

    public int CustomerId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
