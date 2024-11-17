using Jivar.BO.Models;
using Jivar.DAO.DAOs;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class GroupTaskRepository : Repository<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository() : base(GroupTaskDAO.Instance)
        {

        }
    }
}
