using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Work
{
    public int WorkId { get; set; }

    public string WorkLabel32 { get; set; } = null!;

    public string WorkType { get; set; } = null!;

    public string WorkStatus { get; set; } = null!;

    public short WorkLevel { get; set; }

    public string WorkDescription { get; set; } = null!;

    public short WorkDuration { get; set; }

    public short WorkStart { get; set; }

    public short WorkFinish { get; set; }

    public DateTime WorkEntryData { get; set; }

    public int PersonIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public int TaskIdFk { get; set; }

    public short PodCounterWork { get; set; }

    public DateTime WorkStartDate { get; set; }

    public DateTime WorkFinishDate { get; set; }
}
