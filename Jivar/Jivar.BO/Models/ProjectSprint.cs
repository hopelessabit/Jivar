using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class ProjectSprint
{
    public int ProjectId { get; set; }

    public int SprintId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Sprint Sprint { get; set; } = null!;
}
