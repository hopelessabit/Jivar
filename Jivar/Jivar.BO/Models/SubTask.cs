namespace Jivar.BO.Models;

public partial class SubTask
{
    private string v;

    public SubTask(string? title, string? description, string status)
    {
        Title = title;
        Description = description;
        Status = status;
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();
}
