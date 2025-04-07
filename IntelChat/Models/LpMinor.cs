using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LpMinor
{
    public int LpMinorId { get; set; }

    public string LpMinorLabel { get; set; } = null!;

    public string LpMinorDesc { get; set; } = null!;

    public string LpMinorType { get; set; } = null!;

    public string LpMinorStatus { get; set; } = null!;

    public int LpMajorId { get; set; }

    public DateTime? LpMinorDtime { get; set; }

    public int? Location { get; set; }

    public short? LpMinorPersue { get; set; }

    public short? LpMinorAvoid { get; set; }

    public virtual ICollection<LpElement> LpElements { get; } = new List<LpElement>();

    public virtual LpMajor LpMajor { get; set; } = null!;

    public virtual ICollection<WeekMyHistory> WeekMyHistories { get; } = new List<WeekMyHistory>();

    public virtual ICollection<WeekMyTimeslot> WeekMyTimeslots { get; } = new List<WeekMyTimeslot>();
}
