using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class CommentService : ICommentService
    {
        private readonly JivarDbContext _dbContext;

        public CommentService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Comment createComment(Comment comment)
        {
            BO.Models.Task task = _dbContext.Tasks.FirstOrDefault(t => t.Id == comment.TaskId);
            if (task != null)
            {
                _dbContext.Comments.Add(comment);
                _dbContext.SaveChanges();
                return comment;
            }
            return null;
        }
    }
}
