using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class BuyFatora
{
    public int Id { get; set; }

    public int? Idd { get; set; }

    public DateTime? Date { get; set; }

    public string? Customer { get; set; }

    public string? Address { get; set; }

    public string? Mob { get; set; }

    public string? Notes { get; set; }

    public double? Dolar { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public decimal? Payed { get; set; }

    public decimal? Remaing { get; set; }

    public decimal? Tootal { get; set; }

    public string? Mandob { get; set; }

    public double? ManCcount { get; set; }

    public decimal? Ijraaa { get; set; }

    public decimal? Ijraaa2 { get; set; }

    public string? Driver { get; set; }

    public string? CarNo { get; set; }

    public string? DriverMob { get; set; }

    public string? Subb { get; set; }

    public string? EventId { get; set; }

    public decimal? Hamalya { get; set; }

    public DateTime? Datee { get; set; }

    public bool? Checkeed { get; set; }

    public double? Nsba { get; set; }

    public double? Nsbaother { get; set; }

    public string? CostType { get; set; }

    public bool? Notify { get; set; }

    public decimal? Discount { get; set; }

    public int? TotalPoints { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? Hidee { get; set; }

    public int? RemindAfter { get; set; }

    public bool? NotifyMe { get; set; }

    public string? EditedUser { get; set; }

    public DateTime? EditedDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? NoteOther { get; set; }

    public string? Sender { get; set; }

    public string? Bankinfo { get; set; }

    public virtual ICollection<OrderTrackingInvoice> OrderTrackingInvoices { get; set; } = new List<OrderTrackingInvoice>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
