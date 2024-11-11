using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public DateTime? CompleteTime { get; set; }

    public decimal? Budget { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();

    public virtual ICollection<ProjectSprint> ProjectSprints { get; set; } = new List<ProjectSprint>();
}
