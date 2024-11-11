using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class ProjectDAO : BaseDAO<Project>
    {
        private static ProjectDAO? _instance;
        public static ProjectDAO Instance => _instance ??= new ProjectDAO(new JivarDbContext());
        public ProjectDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
