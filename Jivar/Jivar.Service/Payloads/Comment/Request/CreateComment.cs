using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Comment.Request
{
    public class CreateComment
    {
        [Required]
        public string content { get; set; }
        public int? parent_id { get; set; }
    }
}
