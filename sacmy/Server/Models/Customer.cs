using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Customer1 { get; set; }

    public string? Address { get; set; }

    public string? Mob { get; set; }

    public string? Email { get; set; }

    public string? Mandob { get; set; }

    public string? ManAddress { get; set; }

    public string? ManMob { get; set; }

    public double? ManCcount { get; set; }

    public string? Uuser { get; set; }

    public string? Subb { get; set; }

    public string? Costlocat { get; set; }

    public string? CostType { get; set; }

    public double? Nsba { get; set; }

    public double? OtherNsba { get; set; }

    public string? UserAcc { get; set; }

    public string? Password { get; set; }

    public bool? Active { get; set; }

    public string? DeviceId { get; set; }

    public string? FirebaseToken { get; set; }

    public string? RefreshToken { get; set; }

    public bool? IsPlusOneChecked { get; set; }

    public double? PlusOne { get; set; }

    public virtual ICollection<CostumerLocation> CostumerLocations { get; set; } = new List<CostumerLocation>();

    public virtual ICollection<CustomerBillPoint> CustomerBillPoints { get; set; } = new List<CustomerBillPoint>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
