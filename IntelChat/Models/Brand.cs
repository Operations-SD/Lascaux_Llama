using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Brand
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

    public int ProgramIdFk { get; set; }

    public int LocationIdFk { get; set; }

    public string ChannelAlpha { get; set; } = null!;

    public string ChannelBeta { get; set; } = null!;

    public string ChannelGamma { get; set; } = null!;
}
