using Jivar.DAO;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class TaskService : ITaskService
    {
        private readonly JivarDbContext _dbContext;

        public TaskService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BO.Models.Task CreateTask(BO.Models.Task task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            return task;
        }
    }
}
