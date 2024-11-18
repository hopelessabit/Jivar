using Jivar.BO.Models;
using Jivar.Service.Payloads.Sprint.Response;

namespace Jivar.Service.Interfaces
{
    public interface ISprintService
    {
        Task<bool> createSprint(Sprint sprint);
        Task<IEnumerable<Sprint>> listSprints();
        Task<Sprint> getSprintById(int? sprintId);
        Task<bool> updateSprint(Sprint result);
        Task<bool> deleteSprint(Sprint result);
        Task<Sprint?> GetLatestSprint(int projectId, bool? throwExceptionIfNull = false);
        Task<List<Sprint>> GetAllSprintsByProjectIds(List<int> projectIds);
        Task<List<SprintResponse>> GetAllSprintsByProjectIds(List<int> projectIds, bool? includeTask = false);
    }
}
