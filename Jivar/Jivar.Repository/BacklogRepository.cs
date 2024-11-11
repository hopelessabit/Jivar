using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class BacklogRepository : Repository<Backlog>, IBacklogRepository
    {
        public BacklogRepository() : base(BacklogDAO.Instance)
        {
        }
    }
}
