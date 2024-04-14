using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class RewardCustomerLinker
{
    public Guid Id { get; set; }

    public Guid RewardId { get; set; }

    public int CustomerId { get; set; }

    public bool IsApproved { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
