using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ProductSpecification
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? SpecificationId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Specification? Specification { get; set; }
}
