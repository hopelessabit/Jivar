using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class ProjectRole
{
    public int ProjectId { get; set; }

    public int AccountId { get; set; }

    public string? Role { get; set; }

    public virtual Project Account { get; set; } = null!;

    public virtual Account Project { get; set; } = null!;
}
