using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface ITaskDocumentService
    {
        void createTaskDocument(TaskDocument documentTask);
        List<TaskDocument> listTaskDocument(int taskId);
    }
}
