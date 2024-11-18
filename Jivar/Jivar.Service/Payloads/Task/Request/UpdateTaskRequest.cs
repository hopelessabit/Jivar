using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Tasks.Request
{
    public class UpdateTaskRequest
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int? AssignBy { get; set; }

        public int? Assignee { get; set; }

        public int? DocumentId { get; set; }

        //[CompareDate("endDateSprintTask", ErrorMessage = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc")]
        public DateTime? startDateSprintTask { get; set; }

        public DateTime? endDateSprintTask { get; set; }
    }
}
