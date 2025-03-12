using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class BrandArchive
{
    public int BrandId { get; set; }

    public string BrandCode { get; set; } = null!;

    public string BrandLabel { get; set; } = null!;

    public string BrandImage { get; set; } = null!;

    public string BrandStatus { get; set; } = null!;

    public short BrandCntMax { get; set; }

    public short BrandCntReg { get; set; }

    public short BrandEligibility { get; set; }

    public DateTime BrandRegDateClosed { get; set; }

    public decimal BrandCost { get; set; }

    public int BrandGuide { get; set; }

    public string BrandRole { get; set; } = null!;

    public int NovaIdFk { get; set; }

    public int ProgramFk { get; set; }

    public int LocationIdFk { get; set; }

    public int PodId { get; set; }
}
