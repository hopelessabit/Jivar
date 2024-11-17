



using Jivar.Service.Payloads.Task.Request;

namespace Jivar.Service.Interfaces
{
    public interface ITaskService
    {
        BO.Models.Task CreateTask(BO.Models.Task task);
        IEnumerable<BO.Models.Task> getTasks();
        BO.Models.Task getTasksById(int taskId);
        BO.Models.Task updateStatus(int id, string status);
        BO.Models.Task updateTask(int id, UpdateTaskRequest request);
    }
}
