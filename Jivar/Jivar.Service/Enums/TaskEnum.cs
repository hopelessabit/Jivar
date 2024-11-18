using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jivar.Service.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskEnum
    {
        [Display(Name = "Done", Description = "Task đã hoàn thành")]
        DONE,
        [Display(Name = "To Do", Description = "Task cần làm")]
        TO_DO,
        [Display(Name = "In progress", Description = "Task đang thực hiện")]
        IN_PROGRESS,
    }
}
