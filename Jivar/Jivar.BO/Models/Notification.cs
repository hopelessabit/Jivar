namespace Jivar.BO.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string Content { get; set; }

    public DateTime? CreateTime { get; set; }
}
