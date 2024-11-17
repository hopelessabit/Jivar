using Jivar.Service.Payloads.Account.Response;
using ModelProject = Jivar.BO.Models.Project;
using AccountModel = Jivar.BO.Models.Account;
using Jivar.Service.Payloads.ProjectRole.Response;

namespace Jivar.Service.Payloads.Project.Response
{
    public class ProjectResponse
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public AccountInfoResponse CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? CompleteTime { get; set; }

        public decimal? Budget { get; set; }

        public string Status { get; set; }
        public List<ProjectRoleResponse> Roles { get; set; }

        public ProjectResponse(ModelProject project, AccountModel createBy)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            CreateBy = new AccountInfoResponse(createBy);
            CreateTime = project.CreateTime;
            CompleteTime = project.CompleteTime;
            Budget = project.Budget;
            Status = project.Status.ToString();
        }

        public ProjectResponse(ModelProject project, AccountInfoResponse createBy)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            CreateBy = createBy;
            CreateTime = project.CreateTime;
            CompleteTime = project.CompleteTime;
            Budget = project.Budget;
            Status = project.Status.ToString();
        }
    }
}
