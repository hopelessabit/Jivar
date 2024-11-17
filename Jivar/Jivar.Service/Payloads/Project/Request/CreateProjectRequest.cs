using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Payloads.Project.Request
{
    public class CreateProjectRequest
    {
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(1000, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(5000, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value.")]
        public decimal? Budget { get; set; }
    }
}
