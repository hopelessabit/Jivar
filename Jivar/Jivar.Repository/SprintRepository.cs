using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        public SprintRepository() : base(SprintDAO.Instance)
        {
        }
    }
}
