using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Comment.Request
{
    public class UpdateCommentRequest
    {
        [Required]
        public string content { get; set; }
    }
}
