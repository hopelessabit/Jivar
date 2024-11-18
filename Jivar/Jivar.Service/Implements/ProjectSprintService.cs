using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class ProjectSprintService : IProjectSprintService
    {
        private readonly IProjectSprintRepository _projectSprintRepository;

        public ProjectSprintService(IProjectSprintRepository projectSprintRepository)
        {
            _projectSprintRepository = projectSprintRepository;
        }

        // Create a new sprint
        public async Task<bool> AddProjectSprintAsync(ProjectSprint projectSprint)
        {
            return await _projectSprintRepository.AddAsync(projectSprint);
        }

        // Get all sprints for a project
        public async Task<List<ProjectSprint>> GetProjectSprintsByProjectIdAsync(int projectId)
        {
            return (await _projectSprintRepository.GetAllAsync(s => s.ProjectId == projectId)).ToList();
        }

        // Update an existing sprint
        public async Task<bool> ProjectUpdateSprintAsync(ProjectSprint sprint)
        {
            return await _projectSprintRepository.UpdateAsync(sprint);
        }

        // Delete a sprint
        public async Task<bool> ProjectDeleteSprintAsync(int sprintId)
        {
            var sprint = await _projectSprintRepository.GetAsync(s => s.SprintId == sprintId);
            if (sprint == null)
                throw new Exception($"Sprint with ID {sprintId} not found.");

            return await _projectSprintRepository.DeleteAsync(sprint);
        }

        public async Task<List<ProjectSprint>> GetAllProjectSprintsByProjectIds(List<int> projectIds)
        {
            return (await _projectSprintRepository.GetAllAsync(ps => projectIds.Contains(ps.ProjectId))).ToList();
        }
    }
}
