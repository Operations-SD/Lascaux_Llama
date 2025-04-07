using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class Brand
{
    public int Brandid { get; set; }

    public string BrandLink { get; set; } = null!;

    public string BrandName { get; set; } = null!;

  //  public string? BrandImage { get; set; }

    /// <summary>
    /// type collecter Alpha
    /// </summary>
   // public string? ChannelAlpha { get; set; }

  //  public string? ChannelBeta { get; set; }

   // public string? ChannelGamma { get; set; }

    public string? Status { get; set; }

    public short? CntMax { get; set; }

    public short? CntReg { get; set; }

    public short? Eligibility { get; set; }

    public DateTime? RegDateClosed { get; set; }

    public decimal? Cost { get; set; }

    public int? ActiveGuideId { get; set; }

    public int? ProgramId { get; set; }

    public int? LocationId { get; set; }

    public string? MenuLock { get; set; }

    /// <summary>
    /// scoping global variable
    /// </summary>
    public int ScopeLock { get; set; }

    public int? PersonId { get; set; }

    public virtual Person? Person { get; set; }
}
