using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface ISprintService
    {
        Task<bool> createSprint(Sprint sprint);
        Task<IEnumerable<Sprint>> listSprints();
        Task<Sprint> getSprintById(int? sprintId);
        Task<bool> updateSprint(Sprint result);
        Task<bool> deleteSprint(Sprint result);
    }
}
