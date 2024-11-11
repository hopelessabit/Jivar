using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository() : base(CommentDAO.Instance)
        {
        }
    }
}
