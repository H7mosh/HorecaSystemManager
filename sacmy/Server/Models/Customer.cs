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

    public virtual ICollection<AppSessionLog> AppSessionLogs { get; set; } = new List<AppSessionLog>();

    public virtual ICollection<CostumerLocation> CostumerLocations { get; set; } = new List<CostumerLocation>();

    public virtual ICollection<CustomerBillPoint> CustomerBillPoints { get; set; } = new List<CustomerBillPoint>();

    public virtual ICollection<CustomerFavouriteProduct> CustomerFavouriteProducts { get; set; } = new List<CustomerFavouriteProduct>();

    public virtual ICollection<CustomerProductRelation> CustomerProductRelations { get; set; } = new List<CustomerProductRelation>();

    public virtual ICollection<CustomerViewedProduct> CustomerViewedProducts { get; set; } = new List<CustomerViewedProduct>();

    public virtual ICollection<OnlineOrder> OnlineOrders { get; set; } = new List<OnlineOrder>();

    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
