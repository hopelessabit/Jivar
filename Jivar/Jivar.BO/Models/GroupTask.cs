namespace Jivar.BO.Models;

public partial class GroupTask
{

    public int TaskId { get; set; }

    public int SubtaskId { get; set; }

    public virtual SubTask Subtask { get; set; }

    public virtual Task Task { get; set; }

}
