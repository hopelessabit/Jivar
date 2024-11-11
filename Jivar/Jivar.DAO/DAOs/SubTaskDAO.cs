using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class SubTaskDAO : BaseDAO<SubTask>
    {
        private static SubTaskDAO? _instance;
        public static SubTaskDAO Instance => _instance ??= new SubTaskDAO(new JivarDbContext());
        public SubTaskDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
