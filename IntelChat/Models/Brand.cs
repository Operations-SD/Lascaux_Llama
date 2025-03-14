using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandCode { get; set; } = null!;

    public string BrandLabel { get; set; } = null!;

    public string BrandStatus { get; set; } = null!;

    public short BrandCntMax { get; set; }

    public short BrandCntReg { get; set; }

    public DateTime BrandRegDateClosed { get; set; }

    public string BrandRole { get; set; } = null!;

    public short BrandEligibility { get; set; }

    public decimal BrandCost { get; set; }

    public int PersonIdFk { get; set; }

    public int LocationIdFk { get; set; }

    public int ProgramIdFk { get; set; }

    public int GuideIdFk { get; set; }

    public int NovaIdFk { get; set; }

    public int UrlIdFk { get; set; }

    public int PodIdFk { get; set; }

    public string? ChannelAlpha { get; set; }

    public string? ChannelBeta { get; set; }

    public string? ChannelGamma { get; set; }

    public string? BrandImage { get; set; }

    public int? BrandGuide { get; set; }
}
