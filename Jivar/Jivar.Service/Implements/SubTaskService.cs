using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.SubTask.Request;
using Microsoft.EntityFrameworkCore;

namespace Jivar.Service.Implements
{
    public class SubTaskService : ISubTaskService
    {
        private readonly JivarDbContext _dbContext;

        public SubTaskService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubTask> createSubTask(CreateSubTaskRequest request)
        {
            SubTask subTask = new SubTask(request.Title, request.Description, SubTaskEnum.IN_PROGRESS.ToString());
            await _dbContext.AddAsync(subTask);
            await _dbContext.SaveChangesAsync();
            return subTask;
        }

        public async Task<IEnumerable<SubTask>> listGroupTask(int taskId)
        {
            var query = @"
                         SELECT st.*
                         FROM group_task gt
                         JOIN sub_task st
                         ON gt.subtask_id = st.id
                         WHERE gt.task_id = {0}";
            return await _dbContext.SubTasks
                .FromSqlRaw(query, taskId)
                .ToListAsync();
        }

        public async Task<SubTask> listSubTaskByTaskId(int id)
        {
            return await _dbContext.SubTasks.FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task<List<SubTask>> listSubTasks()
        {
            return await _dbContext.SubTasks.ToListAsync();
        }

        public SubTask updateSubTask(int id, UpdateSubTask request, string status)
        {
            SubTask subTask = null;
            subTask = _dbContext.SubTasks.FirstOrDefault(st => st.Id == id);
            if (subTask != null)
            {
                subTask.Title = request.Title;
                subTask.Description = request.Description;
                subTask.Status = status;
                _dbContext.SaveChanges();
            }
            return subTask;
        }
    }
}
