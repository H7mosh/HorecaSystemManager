using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Track
{
    public Guid Id { get; set; }

    public int? CustomerId { get; set; }

    public int? InvoiceId { get; set; }

    public Guid? TypeId { get; set; }

    public Guid? EmployeId { get; set; }

    public string? Note { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employe { get; set; }

    public virtual BuyFatora? Invoice { get; set; }

    public virtual ICollection<TrackComment> TrackComments { get; set; } = new List<TrackComment>();

    public virtual TrackType? Type { get; set; }
}
