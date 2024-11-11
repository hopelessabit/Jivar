using Jivar.DAO.DAOs;
using Task = Jivar.BO.Models.Task;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository() : base(TaskDAO.Instance)
        {
        }
    }
}
