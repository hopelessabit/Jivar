using Jivar.BO.Enumarate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Payloads.Project.Request
{
    public class UpdateProjectRequest
    {

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal? Budget { get; set; }
        public ProjectStatus? Status { get; set; }
    }
}
