using Jivar.BO.Models;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Comment.Request;
using Jivar.Service.Payloads.Comment.Response;

namespace Jivar.Service.Interfaces
{
    public interface ICommentService
    {
        Comment createComment(Comment comment);
        Comment createReplayComment(int commetId, ReplayComment request, int userId);
        bool getCommentById(int id);
        IEnumerable<Comment> getCommentByTaskId(int taskId);
        bool updateCommentById(int id, UpdateCommentRequest request);
        List<CommentResponse> GetCommentByTaskId(int taskId, List<AccountInfoResponse> account);
    }
}
