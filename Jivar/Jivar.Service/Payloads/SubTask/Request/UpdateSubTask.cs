using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.SubTask.Request
{
    public class UpdateSubTask
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

    }
}
