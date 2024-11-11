using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class ProjectRoleDAO : BaseDAO<ProjectRole>
    {
        private static ProjectRoleDAO? _instance;
        public static ProjectRoleDAO Instance => _instance ??= new ProjectRoleDAO(new JivarDbContext());
        public ProjectRoleDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
