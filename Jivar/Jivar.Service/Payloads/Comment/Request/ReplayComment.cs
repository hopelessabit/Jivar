using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Comment.Request
{
    public class ReplayComment
    {
        [Required]
        public string content { get; set; }

        [Required]
        public int taskId { get; set; }
    }
}
