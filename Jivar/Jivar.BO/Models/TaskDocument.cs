namespace Jivar.BO.Models;

public partial class TaskDocument
{
    public TaskDocument(int taskId, int documentId)
    {
        TaskId = taskId;
        DocumentId = documentId;
    }

    public int TaskId { get; set; }

    public int DocumentId { get; set; }

    public virtual Document Document { get; set; }

    public virtual Task Task { get; set; }
}
