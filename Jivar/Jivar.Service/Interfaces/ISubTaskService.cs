using Jivar.BO.Models;
using Jivar.Service.Payloads.SubTask.Request;

namespace Jivar.Service.Interfaces
{
    public interface ISubTaskService
    {
        Task<SubTask> createSubTask(CreateSubTaskRequest request);
    }
}
