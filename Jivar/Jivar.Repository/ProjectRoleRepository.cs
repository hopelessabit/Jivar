using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class ProjectRoleRepository : Repository<ProjectRole>, IProjectRoleRepository
    {
        public ProjectRoleRepository() : base(ProjectRoleDAO.Instance)
        {
        }
    }
}
