﻿using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ProductImage
{
    public Guid Id { get; set; }

    public string? ImageLink { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
