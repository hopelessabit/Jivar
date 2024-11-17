namespace Jivar.BO.Models;

public partial class TaskDocument
{
    public int TaskId { get; set; }

    public int DocumentId { get; set; }

    public virtual Document Document { get; set; }

    public virtual Task Task { get; set; }
}
