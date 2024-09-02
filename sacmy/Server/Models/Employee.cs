using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Code { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? JobTitle { get; set; }

    public string? JobTitleAr { get; set; }

    public string? JobTitleTr { get; set; }

    public string? JobTitleKr { get; set; }

    public string? Branch { get; set; }

    public string? BranchAr { get; set; }

    public string? BranchTr { get; set; }

    public string? BranchKr { get; set; }

    public string? Brand { get; set; }

    public string? Image { get; set; }

    public string? FirebaseToken { get; set; }

    public Guid? RoleId { get; set; }

    public bool? Shown { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CanclledDate { get; set; }

    public virtual ICollection<HorecaInformation> HorecaInformations { get; set; } = new List<HorecaInformation>();

    public virtual EmpolyeeRole? Role { get; set; }

    public virtual ICollection<Task> TaskAssignedToEmployeeNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskCreatedByNavigations { get; set; } = new List<Task>();

    public virtual ICollection<TaskNote> TaskNotes { get; set; } = new List<TaskNote>();

    public virtual ICollection<TrackComment> TrackCommentAssignedToNavigations { get; set; } = new List<TrackComment>();

    public virtual ICollection<TrackComment> TrackCommentEmployees { get; set; } = new List<TrackComment>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
