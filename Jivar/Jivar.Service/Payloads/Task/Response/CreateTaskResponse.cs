namespace Jivar.Service.Payloads.Tasks.Response
{
    public class CreateTaskResponse
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? AssignBy { get; set; }

        public int? Assignee { get; set; }

        public DateTime? CompleteTime { get; set; }

        public int? DocumentId { get; set; }
        public string? Status { get; set; }
    }
}
