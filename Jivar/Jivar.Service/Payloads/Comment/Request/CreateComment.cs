using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Comment.Request
{
    public class CreateComment
    {
        [Required]
        public string content;
        public int? parent_id;
    }
}
