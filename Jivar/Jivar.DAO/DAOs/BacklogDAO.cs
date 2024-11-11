using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class BacklogDAO : BaseDAO<Backlog>
    {
        private static BacklogDAO? _instance;
        public static BacklogDAO Instance => _instance ??= new BacklogDAO(new JivarDbContext());
        public BacklogDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
