using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class WeeklyAnalytic
{
    public int WeekId { get; set; }

    public int? PersonId { get; set; }

    public int NounIdFk { get; set; }

    public string? PersonLast { get; set; }

    public int ElementId { get; set; }

    public string NounLabel { get; set; } = null!;

    public string ElementLabel { get; set; } = null!;

    public short Quantity { get; set; }

    public int ElementInt { get; set; }

    public int? ChartData { get; set; }

    public string NounType { get; set; } = null!;

    public string ElementType { get; set; } = null!;

    public short Group { get; set; }
}
