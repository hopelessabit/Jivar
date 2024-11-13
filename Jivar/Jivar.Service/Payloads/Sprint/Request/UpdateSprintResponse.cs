using Jivar.Service.Util;
using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.Sprint.Request
{
    public class UpdateSprintResponse
    {
        public string? Name { get; set; }

        [DateLessThanOrEqualToToday]
        [CompareDate("EndDate", ErrorMessage = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc")]
        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}
