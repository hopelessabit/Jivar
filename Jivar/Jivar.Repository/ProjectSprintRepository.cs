using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class ProjectSprintRepository : Repository<ProjectSprint>, IProjectSprintRepository
    {
        public ProjectSprintRepository() : base(ProjectSprintDAO.Instance)
        {
        }
    }
}
