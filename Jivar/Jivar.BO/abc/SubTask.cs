using System;
using System.Collections.Generic;

namespace Jivar.BO.abc;

public partial class SubTask
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
