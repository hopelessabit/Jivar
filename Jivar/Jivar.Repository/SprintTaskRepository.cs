using Jivar.BO.Models;
using Jivar.DAO.DAOs;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class SprintTaskRepository : Repository<SprintTask>, ISprintTaskRepository
    {
        public SprintTaskRepository() : base(SprintTaskDAO.Instance)
        {
        }

        public Task<bool> DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
