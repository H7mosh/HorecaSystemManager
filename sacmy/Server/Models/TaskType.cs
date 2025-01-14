using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class TaskType
{
    public Guid Id { get; set; }

    public string TypeEn { get; set; } = null!;

    public string? TypeAr { get; set; }

    public string? TypeTr { get; set; }

    public string? TypeKr { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
