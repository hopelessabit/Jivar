using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class TaskDocumentService : ITaskDocumentService
    {
        private readonly JivarDbContext _dbContext;
        public TaskDocumentService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void createTaskDocument(TaskDocument documentTask)
        {
            _dbContext.TaskDocuments.Add(documentTask);
            _dbContext.SaveChanges();
        }

        public List<TaskDocument> listTaskDocument(int taskId)
        {
            return _dbContext.TaskDocuments.Where(d => d.TaskId == taskId).ToList();
        }
    }
}
