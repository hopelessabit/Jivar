using Jivar.BO.Enumarate;
using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Payloads.ProjectRole.Request
{
    public class CreateProjectRoleRequest
    {
        [Required(ErrorMessage = "Account's Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Account's Id must be greater than 0.")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Project's Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Project's Id must be greater than 0.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [EnumDataType(typeof(ProjectRoleType), ErrorMessage = "Invalid role specified. Please provide a valid ProjectRoleType.")]
        public ProjectRoleType Role { get; set; }
    }
}
