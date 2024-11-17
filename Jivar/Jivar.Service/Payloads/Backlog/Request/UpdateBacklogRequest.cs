using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Payloads.Backlog.Request
{
    public class UpdateBacklogRequest
    {
        public int? ProjectId { get; set; }

        [StringLength(1000, ErrorMessage = "Content must be less than 500 characters.")]
        public string? Content { get; set; }

        public int? TaskId { get; set; }

        public int? Assignee { get; set; }
    }
}
