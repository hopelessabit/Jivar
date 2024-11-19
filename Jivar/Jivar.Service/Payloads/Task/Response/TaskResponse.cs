using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Comment.Response;
using Jivar.Service.Payloads.Project.Response;
using AccountModel = Jivar.BO.Models.Account;
using TaskModel = Jivar.BO.Models.Task;

namespace Jivar.Service.Payloads.Tasks.Response
{
    public class TaskResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? CreateBy { get; set; }

        public string? CreateByName { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? AssignBy { get; set; }

        public string? AssignByName { get; set; }

        public int? Assignee { get; set; }

        public string? AssigneeName { get; set; }

        public DateTime? CompleteTime { get; set; }
        public DateTime? StartDateSprintTask { get; set; }
        public DateTime? EndDateSprintTask { get; set; }

        public int? DocumentId { get; set; }

        public string Status { get; set; }

        public List<CommentResponse> Comments { get; set; }

        public List<DocumentResponse> Documents { get; set; }

        public TaskResponse(TaskModel task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            CreateBy = task.CreateBy;
            CreateTime = task.CreateTime;
            AssignBy = task.AssignBy;
            Assignee = task.Assignee;
            CompleteTime = task.CompleteTime;
            StartDateSprintTask = task.SprintTask != null ? task.SprintTask.StartDate : null;
            EndDateSprintTask = task.SprintTask != null ? task.SprintTask.EndDate : null;
            DocumentId = task.DocumentId;
            Status = task.Status;
        }
        public TaskResponse(TaskModel task, AccountModel account)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            CreateBy = task.CreateBy;
            CreateTime = task.CreateTime;
            AssignBy = task.AssignBy;
            Assignee = task.Assignee;
            CompleteTime = task.CompleteTime;
            StartDateSprintTask = task.SprintTask != null ? task.SprintTask.StartDate : null;
            EndDateSprintTask = task.SprintTask != null ? task.SprintTask.EndDate : null;
            DocumentId = task.DocumentId;
            Status = task.Status;
            AssigneeName = account.Name;
        }
        public TaskResponse(TaskModel task,
            AccountInfoResponse createBy,
            List<CommentResponse> comments,
            AccountInfoResponse? assignee = null,
            AccountInfoResponse? assignBy = null)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            CreateBy = task.CreateBy;
            CreateByName = createBy == null ? null : createBy.Name;
            CreateTime = task.CreateTime;
            AssignBy = task.AssignBy;
            Assignee = task.Assignee;
            CompleteTime = task.CompleteTime;
            StartDateSprintTask = task.SprintTask != null ? task.SprintTask.StartDate : null;
            EndDateSprintTask = task.SprintTask != null ? task.SprintTask.EndDate : null;
            CompleteTime = task.CompleteTime;
            DocumentId = task.DocumentId;
            Status = task.Status;
            AssigneeName = assignee == null ? null : assignee.Name ;
            AssignByName = assignBy == null ? null : assignBy.Name ;
            Comments = comments;
        }
    }
}
