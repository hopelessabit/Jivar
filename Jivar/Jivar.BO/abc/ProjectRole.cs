using System;
using System.Collections.Generic;

namespace Jivar.BO.abc;

public partial class ProjectRole
{
    public int ProjectId { get; set; }

    public int AccountId { get; set; }

    public string? Role { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
