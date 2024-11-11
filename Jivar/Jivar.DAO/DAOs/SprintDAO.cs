using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class SprintDAO : BaseDAO<Sprint>
    {
        private static SprintDAO? _instance;
        public static SprintDAO Instance => _instance ??= new SprintDAO(new JivarDbContext());
        public SprintDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
