using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public int? AssignBy { get; set; }

    public int? Assignee { get; set; }

    public DateTime? CompleteTime { get; set; }

    public int? DocumentId { get; set; }

    public string Status { get; set; }

    public virtual ICollection<Backlog> Backlogs { get; set; } = new List<Backlog>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();

    public virtual ICollection<SprintTask> SprintTasks { get; set; } = new List<SprintTask>();

    public virtual ICollection<TaskDocument> TaskDocuments { get; set; } = new List<TaskDocument>();
}
