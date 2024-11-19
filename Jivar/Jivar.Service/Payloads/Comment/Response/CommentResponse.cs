using Jivar.Service.Payloads.Account.Response;
using CommentModel = Jivar.BO.Models.Comment;

namespace Jivar.Service.Payloads.Comment.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int TaskId { get; set; }

        public int CreateBy { get; set; }
        public string CreateByName {  get; set; }

        public int? ParentId { get; set; }

        public DateTime CreateTime { get; set; }
        public string Status { get; set; }

        public List<CommentResponse> Replies { get; set; }


        public CommentResponse(CommentModel comment, AccountInfoResponse account)
        {
            Id = comment.Id;
            Content = comment.Content;
            TaskId = comment.TaskId;
            CreateBy = comment.CreateBy;
            CreateByName = account.Name;
            ParentId = comment.ParentId;
            CreateTime = comment.CreateTime;
            Status = comment.Status;
        }
        public CommentResponse()
        {
        }
    }
}
