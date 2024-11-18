using SprintModel = Jivar.BO.Models.Sprint;
using Jivar.Service.Payloads.Tasks.Response;

namespace Jivar.Service.Payloads.Sprint.Response
{
    public class SprintResponse
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<TaskResponse> Tasks { get; set; }

        public SprintResponse(SprintModel sprint)
        {
            Id = sprint.Id;
            ProjectId = sprint.ProjectId;
            Name = sprint.Name;
            StartDate = sprint.StartDate;
            EndDate = sprint.EndDate;
            this.Tasks = null;
        }
    }
}
