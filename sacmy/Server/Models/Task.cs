using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Task
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid? AssignedToEmployee { get; set; }

    public int? CustomerId { get; set; }

    public int? InvoiceId { get; set; }

    public Guid TypeId { get; set; }

    public Guid? StatusId { get; set; }

    public DateTime? Deadline { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Employee? AssignedToEmployeeNavigation { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual BuyFatora? Invoice { get; set; }

    public virtual Status? Status { get; set; }

    public virtual ICollection<TaskNote> TaskNotes { get; set; } = new List<TaskNote>();

    public virtual TaskType Type { get; set; } = null!;
}
