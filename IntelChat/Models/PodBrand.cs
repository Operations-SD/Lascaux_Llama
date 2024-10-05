using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class PodBrand
{
    public int PodId { get; set; }

    public string PodLabel { get; set; } = null!;

    public string BrandCode { get; set; } = null!;

    public string BrandLabel { get; set; } = null!;

    public string BrandRole { get; set; } = null!;

    public int PodIdFk { get; set; }
}
