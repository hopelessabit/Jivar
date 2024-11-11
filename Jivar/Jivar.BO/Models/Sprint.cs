using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class Sprint
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<ProjectSprint> ProjectSprints { get; set; } = new List<ProjectSprint>();

    public virtual ICollection<SprintTask> SprintTasks { get; set; } = new List<SprintTask>();
}
