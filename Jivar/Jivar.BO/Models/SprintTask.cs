namespace Jivar.BO.Models;

public partial class SprintTask
{
    public int SprintId { get; set; }

    public int TaskId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public string? Status { get; set; }

    public virtual Sprint Sprint { get; set; }

    public virtual Task Task { get; set; }
}
