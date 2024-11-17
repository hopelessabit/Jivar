using Jivar.BO;
using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class SprintTaskDAO : BaseDAO<SprintTask>
    {
        private static SprintTaskDAO? _instance;
        public static SprintTaskDAO Instance => _instance ??= new SprintTaskDAO(new JivarDbContext());
        public SprintTaskDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
