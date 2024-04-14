using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Userlogin
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public DateTime? Fulldate { get; set; }

    public string? Per { get; set; }
}
