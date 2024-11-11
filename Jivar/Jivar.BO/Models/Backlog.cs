using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class Backlog
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? Content { get; set; }

    public int? CreateBy { get; set; }

    public int? TaskId { get; set; }

    public int? Assignee { get; set; }

    public DateTime? CreateTime { get; set; }

    public virtual Task? Task { get; set; }
}
