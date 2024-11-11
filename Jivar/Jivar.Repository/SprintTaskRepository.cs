using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class SprintTaskRepository : Repository<SprintTask>, ISprintTaskRepository
    {
        public SprintTaskRepository() : base(SprintTaskDAO.Instance)
        {
        }
    }
}
