using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class EmpolyeeRole
{
    public Guid Id { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
