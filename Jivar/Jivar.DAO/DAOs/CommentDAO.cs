using Jivar.BO;
using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class CommentDAO : BaseDAO<Comment>
    {
        private static CommentDAO? _instance;
        public static CommentDAO Instance => _instance ??= new CommentDAO(new JivarDbContext());
        public CommentDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
