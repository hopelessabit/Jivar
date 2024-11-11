using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class ProjectSprintDAO : BaseDAO<ProjectSprint>
    {
        private static ProjectSprintDAO? _instance;
        public static ProjectSprintDAO Instance => _instance ??= new ProjectSprintDAO(new JivarDbContext());
        public ProjectSprintDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
