using Jivar.BO;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Tasks.Request;

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

        public IEnumerable<BO.Models.Task> getTasks()
        {
            return _dbContext.Tasks.ToList();
        }

        public BO.Models.Task getTasksById(int taskId)
        {
            return _dbContext.Tasks.FirstOrDefault(task => task.Id.Equals(taskId));
        }

        public BO.Models.Task updateStatus(int id, string status)
        {
            BO.Models.Task task = null;
            task = _dbContext.Tasks.FirstOrDefault(task => task.Id.Equals(id));
            if (task != null)
            {
                if (task.Status.Equals(TaskEnum.COMPLETE.ToString()) || task.Status.Equals(TaskEnum.NOT_COMPLETE.ToString()))
                {
                    task.CompleteTime = DateTime.UtcNow;
                }
                else if (task.Status.Equals(TaskEnum.IN_PROGRESS.ToString()))
                {
                    task.CompleteTime = null;
                }
                task.Status = status;
                _dbContext.SaveChanges();
            }
            return task;
        }

        public BO.Models.Task updateTask(int id, UpdateTaskRequest request)
        {
            BO.Models.Task task = null;
            task = _dbContext.Tasks.FirstOrDefault(task => task.Id.Equals(id));
            if (task != null)
            {
                task.Title = request.Title;
                task.Description = request.Description;
                task.AssignBy = request.AssignBy;
                task.Assignee = request.Assignee;
                task.DocumentId = request.DocumentId;
                _dbContext.SaveChanges();
            }
            return task;
        }
    }
}
