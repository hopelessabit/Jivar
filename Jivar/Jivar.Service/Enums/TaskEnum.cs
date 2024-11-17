using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jivar.Service.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskEnum
    {
        [Display(Name = "Complete", Description = "Task đã hoàn thành")]
        COMPLETE,
        [Display(Name = "Not Complete", Description = "Task chưa hoàn thành")]
        NOT_COMPLETE,
        [Display(Name = "In progress", Description = "Task đang thực hiện")]
        IN_PROGRESS,
    }
}
