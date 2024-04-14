using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CustomerBillPoint
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }

    public int BillId { get; set; }

    public int Points { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
