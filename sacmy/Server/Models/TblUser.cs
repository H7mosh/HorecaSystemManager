using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Per { get; set; }

    public string? Subb { get; set; }
}
