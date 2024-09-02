using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CustomerViewedProduct
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
