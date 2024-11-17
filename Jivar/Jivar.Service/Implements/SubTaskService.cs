using Jivar.BO.Models;
using Jivar.DAO;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.SubTask.Request;

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
    }
}
