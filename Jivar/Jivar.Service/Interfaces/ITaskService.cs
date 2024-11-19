



using Jivar.Service.Payloads.Tasks.Request;
using Jivar.Service.Payloads.Tasks.Response;

namespace Jivar.Service.Interfaces
{
    public interface ITaskService
    {
        BO.Models.Task CreateTask(BO.Models.Task task);
        IEnumerable<BO.Models.Task> getTasks();
        TaskResponse getTasksById(int taskId);
        BO.Models.Task updateStatus(int id, string status);
        BO.Models.Task updateTask(int id, UpdateTaskRequest request);
    }
}
