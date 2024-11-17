using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Backlog.Request
{
    public class CreateBacklogRequest
    {

        [Required(ErrorMessage = "ProjectId is required.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(1000, ErrorMessage = "Content must be less than 500 characters.")]
        public string Content { get; set; }

        public int? TaskId { get; set; }

        public int? Assignee { get; set; }
    }
}
