using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jivar.Service.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SubTaskEnum
    {
        [Display(Name = "Complete", Description = "subTask đã hoàn thành")]
        COMPLETE,
        [Display(Name = "Not Complete", Description = "subTask chưa hoàn thành")]
        NOT_COMPLETE,
        [Display(Name = "In progress", Description = "subTask đang thực hiện")]
        IN_PROGRESS,
    }
}
