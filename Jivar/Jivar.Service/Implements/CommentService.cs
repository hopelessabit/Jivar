using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Comment.Request;
using Jivar.Service.Payloads.Comment.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public Comment createReplayComment(int commentId, ReplayComment request, int userId)
        {
            Comment comment = _dbContext.Comments.FirstOrDefault(t => t.Id == commentId);
            if (comment != null)
            {
                Comment replay = new Comment();
                replay.Content = request.content;
                replay.ParentId = commentId;
                replay.TaskId = request.taskId;
                replay.CreateBy = userId;
                replay.CreateTime = DateTime.Now;
                replay.Status = CommentStatus.ACTIVE.ToString();
                _dbContext.Comments.Add(replay);
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
                      .ThenInclude(c => c.Replies.Where(r => r.Status == "ACTIVE"))
                      .ToList();
        }

        public List<CommentResponse> GetCommentByTaskId(int taskId, List<AccountInfoResponse> account)
        {
            IEnumerable<Comment> allComments = _dbContext.Comments
                      .Where(c => c.TaskId == taskId && c.Status == "ACTIVE")
                      .ToList();
            IEnumerable<Comment> rootComments = allComments.Where(c => c.ParentId == null);
            return rootComments.Select(c => MapToCommentResponse(c, account, allComments)).ToList();
        }
        private CommentResponse MapToCommentResponse(Comment comment, List<AccountInfoResponse> account, IEnumerable<Comment> allComments)
        {
            return new CommentResponse()
            {
                Id = comment.Id,
                Content = comment.Content,
                TaskId = comment.TaskId,
                CreateBy = comment.CreateBy,
                CreateByName = account.Find(a => a.Id == comment.CreateBy).Name,
                CreateTime = comment.CreateTime,
                ParentId = comment.ParentId,
                Replies = allComments
                    .Where(c => c.ParentId == comment.Id) // Find replies for this comment
                    .Select(c => MapToCommentResponse(c, account, allComments)) // Recursively map each reply
                    .ToList()
            };
        }
        public bool updateCommentById(int id)
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

        public bool updateCommentById(int id, UpdateCommentRequest request)
        {
            Comment comment = _dbContext.Comments.FirstOrDefault(t => t.Id == id);
            if (comment != null)
            {
                comment.Content = request.content;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
