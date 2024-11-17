
namespace Jivar.Service.Interfaces
{
    public interface IGroupTaskService
    {
        Task<bool> createGroupTask(int taskId, int id);
    }
}
