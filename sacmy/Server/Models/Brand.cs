using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Brand
{
    public Guid Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? NameTr { get; set; }

    public string? NameKr { get; set; }

    public string? Image { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}
