using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public bool getCommentById(int id)
        {
            Comment comment = _dbContext.Comments.FirstOrDefault(t => t.Id == id);
            if (comment != null)
            {
                comment.Status = CommentStatus.INACTIVE.ToString();
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Comment> getCommentByTaskId(int taskId)
        {
            return _dbContext.Comments
                      .Where(c => c.TaskId == taskId && c.ParentId == null && c.Status == "ACTIVE")
                      .Include(c => c.Replies.Where(r => r.Status == "ACTIVE"))
                      .ToList();
        }


    }
}
