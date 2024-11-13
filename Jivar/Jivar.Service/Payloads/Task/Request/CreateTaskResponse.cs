namespace Jivar.Service.Payloads.Task.Request
{
    public class CreateTaskRequest
    {

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? AssignBy { get; set; }

        public int? Assignee { get; set; }

        public DateTime? CompleteTime { get; set; }

        public int? DocumentId { get; set; }
    }
}
