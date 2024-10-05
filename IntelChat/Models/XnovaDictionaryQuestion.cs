using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class XnovaDictionaryQuestion
{
	public string About { get; set; } = null!;

	public int P { get; set; }

	public int N { get; set; }

	public string NovaDescription { get; set; } = null!;

	public int S { get; set; }

	public string Subject { get; set; } = null!;

	public string SubjectDescription { get; set; } = null!;

	public string SubjectUrl { get; set; } = null!;

	public int A { get; set; }

	public string Action { get; set; } = null!;

	public string ActionDescription { get; set; } = null!;

	public string ActionUrl { get; set; } = null!;

	public int O { get; set; }

	public string Object { get; set; } = null!;

	public string ObjectDescription { get; set; } = null!;

	public string ObjectUrl { get; set; } = null!;

	public int Pid { get; set; }
}
