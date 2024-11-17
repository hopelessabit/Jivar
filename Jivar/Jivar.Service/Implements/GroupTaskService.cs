using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class GroupTaskService : IGroupTaskService
    {

        private readonly JivarDbContext _dbContext;

        public GroupTaskService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> createGroupTask(int taskId, int subTaskId)
        {

            GroupTask group = new GroupTask();
            group.SubtaskId = subTaskId;
            group.TaskId = taskId;
            if ((_dbContext.Tasks.FirstOrDefault(t => t.Id.Equals(taskId)) != null))
            {
                _dbContext.GroupTasks.Add(group);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
