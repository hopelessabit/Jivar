



using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Tasks.Request;
using Jivar.Service.Payloads.Tasks.Response;
using Microsoft.AspNetCore.Http;

namespace Jivar.Service.Interfaces
{
    public interface ITaskService
    {
        BO.Models.Task CreateTask(BO.Models.Task task);
        IEnumerable<BO.Models.Task> getTasks();
        TaskResponse getTasksById(int taskId);
        BO.Models.Task updateStatus(int id, string status);
        Task<BO.Models.Task> updateStatusV2(int id, string status);
        Task<BO.Models.Task> updateTask(int id, UpdateTaskRequest request);
        Task<BO.Models.Task> updateTaskV2(int id, UpdateTaskRequest request);
        Task<TaskResponse> GetTasksById(int taskId, int projectId, HttpContext context);
        Task<List<TaskResponse>> GetTasksByIds(List<int> taskId, int projectId);
    }
}
