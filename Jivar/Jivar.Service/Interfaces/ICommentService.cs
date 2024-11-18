using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface ICommentService
    {
        Comment createComment(Comment comment);
        bool getCommentById(int id);
        IEnumerable<Comment> getCommentByTaskId(int taskId);
    }
}
