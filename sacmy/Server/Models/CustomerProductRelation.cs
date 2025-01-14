using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CustomerProductRelation
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }

    public Guid ProductId { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public bool IsDiscounted { get; set; }

    public decimal? RaisePercentage { get; set; }

    public bool IsRaised { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
