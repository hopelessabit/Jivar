using Jivar.BO.Models;
using Jivar.Service.Payloads.SubTask.Request;

namespace Jivar.Service.Interfaces
{
    public interface ISubTaskService
    {
        Task<SubTask> createSubTask(CreateSubTaskRequest request);
        Task<List<SubTask>> listSubTasks();
        Task<SubTask> listSubTaskByTaskId(int id);
        Task<IEnumerable<SubTask>> listGroupTask(int taskId);
        SubTask updateSubTask(int id, UpdateSubTask request, string status);
    }
}
