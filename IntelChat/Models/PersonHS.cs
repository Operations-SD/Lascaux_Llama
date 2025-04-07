using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Person
{
   // public int PersonId { get; set; }

    public string? PersonPw { get; set; }

    public string? Person1 { get; set; }

    public string? NameFirst { get; set; }

    public string? NameLast { get; set; }

    /// <summary>
    /// &quot;branded role&quot; to determine home menu person is locked into
    /// </summary>
    public string? Type { get; set; }

    public bool? Host { get; set; }

    public string? Status { get; set; }

    public int? LocationId { get; set; }

    public int? AccountId { get; set; }

    public bool? OnLine { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public DateTime? DateEntry { get; set; }

    public virtual ICollection<Brand> Brands { get; } = new List<Brand>();

    public virtual ICollection<LpMajor> LpMajors { get; } = new List<LpMajor>();

    public virtual ICollection<WeekMyHistory> WeekMyHistories { get; } = new List<WeekMyHistory>();

    public virtual ICollection<WeekMyTimeslot> WeekMyTimeslots { get; } = new List<WeekMyTimeslot>();
}
