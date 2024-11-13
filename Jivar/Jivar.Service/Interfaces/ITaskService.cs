
namespace Jivar.Service.Interfaces
{
    public interface ITaskService
    {
        Task<bool> CreateTask(BO.Models.Task task);
    }
}
