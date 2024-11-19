using Jivar.BO.Models;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Comment.Response;
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

        public DateTime? CreateTime { get; set; }

        public int? AssignBy { get; set; }

        public int? Assignee { get; set; }

        public string? AssigneeName {  get; set; }

        public DateTime? CompleteTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? DocumentId { get; set; }

        public string Status { get; set; }

        public List<CommentResponse> Comments { get; set; }

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
            StartDate = task.SprintTask != null ? task.SprintTask.StartDate : null;
            EndDate = task.SprintTask != null ? task.SprintTask.EndDate : null;
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
            StartDate = task.SprintTask != null ? task.SprintTask.StartDate : null;
            EndDate = task.SprintTask != null ? task.SprintTask.EndDate : null;
            DocumentId = task.DocumentId;
            Status = task.Status;
            AssigneeName = account.Name;
        }
        public TaskResponse(TaskModel task, AccountInfoResponse account, List<CommentResponse> comments)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            CreateBy = task.CreateBy;
            CreateTime = task.CreateTime;
            AssignBy = task.AssignBy;
            Assignee = task.Assignee;
            CompleteTime = task.CompleteTime;
            StartDate = task.SprintTask != null ? task.SprintTask.StartDate : null;
            EndDate = task.SprintTask != null ? task.SprintTask.EndDate : null;
            CompleteTime = task.CompleteTime;
            DocumentId = task.DocumentId;
            Status = task.Status;
            AssigneeName = account.Name;
            Comments = comments;
        }
    }
}
