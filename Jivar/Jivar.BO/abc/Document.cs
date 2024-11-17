using System;
using System.Collections.Generic;

namespace Jivar.BO.abc;

public partial class Document
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadDate { get; set; }

    public int? UploadBy { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
