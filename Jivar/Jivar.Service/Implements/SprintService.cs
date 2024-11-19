using Google.Apis.Util;
using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Sprint.Response;
using Jivar.Service.Payloads.Tasks.Response;

namespace Jivar.Service.Implements
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly ISprintTaskRepository _sprintTaskRepository;
        private readonly ITaskRepository _taskRepository;
        public SprintService(ISprintRepository sprintRepository,
            ISprintTaskRepository sprintTaskRepository,
            ITaskRepository taskRepository)
        {
            _sprintRepository = sprintRepository;
            _sprintTaskRepository = sprintTaskRepository;
            _taskRepository = taskRepository;
        }

        public async Task<bool> createSprint(Sprint sprint)
        {
            return await _sprintRepository.AddAsync(sprint);
        }

        public async Task<IEnumerable<Sprint>> listSprints()
        {
            return await _sprintRepository.GetAllAsync();
        }

        public async Task<Sprint> getSprintById(int? sprintId)
        {
            var result = await _sprintRepository.GetAsync(sp => sp.Id == sprintId);
            return result != null ? result : null;
        }

        public async Task<bool> updateSprint(Sprint result)
        {
            return await _sprintRepository.UpdateAsync(result);
        }

        public async Task<bool> deleteSprint(Sprint result)
        {
            return await _sprintRepository.DeleteAsync(result);
        }

        public async Task<Sprint?> GetLatestSprint(int projectId, bool? throwExceptionIfNull = false)
        {
            Sprint? result = (await _sprintRepository.GetAllAsync(s => s.ProjectId == projectId)).OrderByDescending(s => s.Id).FirstOrDefault();
            if (throwExceptionIfNull.Value && result == null)
                throw new ArgumentNullException($"Latest Sprint for Project with Id: {projectId}");
            return result;
        }

        public async Task<List<Sprint>> GetAllSprintsByProjectIds(List<int> projectIds)
        {
            return (await _sprintRepository.GetAllAsync(s => projectIds.Contains(s.ProjectId))).ToList();
        }

        public async Task<List<SprintResponse>> GetAllSprintsByProjectIds(List<int> projectIds, bool? includeTask = false)
        {
            List<SprintResponse> sprints = (await _sprintRepository.GetAllAsync(s => projectIds.Contains(s.ProjectId))).Select(s => new SprintResponse(s)).ToList();
            if (includeTask.Value)
            {
                List<SprintTask> sprintTasks = (await _sprintTaskRepository.GetAllAsync(st => sprints.Select(s => s.Id).ToList().Contains(st.SprintId))).ToList();
                if (!sprintTasks.Any())
                    return sprints;
                List<TaskResponse> taskResponses = (await _taskRepository.GetAllAsync(st => sprintTasks.Select(st => st.TaskId).Distinct().ToList().Contains(st.Id), "SprintTask")).Select(t => new TaskResponse(t)).ToList();

                foreach (var item in sprints)
                {
                    List<SprintTask> sprintTasksForSprint = sprintTasks.FindAll(st => st.SprintId == item.Id).ToList();
                    if (!sprintTasksForSprint.Any())
                        continue;
                    List<TaskResponse> taskResponsesForSprint = taskResponses.FindAll(t => sprintTasksForSprint.Select(s => s.TaskId).ToList().Contains(t.Id)).ToList();
                    item.Tasks = taskResponsesForSprint;
                }
            }

            return sprints;
        }
    }
}
