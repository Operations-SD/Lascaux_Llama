using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MyAnswer
{
    public int PersonIdFk { get; set; }

    public int QuestionIdFk { get; set; }

    public byte MyResponse { get; set; }

    public byte MySeverity { get; set; }

    public DateTime DateIview { get; set; }
}
