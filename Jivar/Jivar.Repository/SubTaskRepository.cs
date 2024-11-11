using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class SubTaskRepository : Repository<SubTask>, ISubTaskRepository
    {
        public SubTaskRepository() : base(SubTaskDAO.Instance)
        {
        }
    }
}
