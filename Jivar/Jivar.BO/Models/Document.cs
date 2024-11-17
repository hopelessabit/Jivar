namespace Jivar.BO.Models;

public partial class Document
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string FilePath { get; set; }

    public DateTime? UploadDate { get; set; }

    public int? UploadBy { get; set; }

    public virtual ICollection<TaskDocument> TaskDocuments { get; set; } = new List<TaskDocument>();
}
