using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface IProjectSprintService
    {
        /// <summary>
        /// Adds a new sprint.
        /// </summary>
        /// <param name="projectSprint">The sprint to be added.</param>
        /// <returns>True if the sprint is added successfully; otherwise, false.</returns>
        Task<bool> AddProjectSprintAsync(ProjectSprint projectSprint);

        /// <summary>
        /// Retrieves all sprints for a specific project.
        /// </summary>
        /// <param name="projectId">The ID of the project.</param>
        /// <returns>A list of sprints associated with the project.</returns>
        Task<List<ProjectSprint>> GetProjectSprintsByProjectIdAsync(int projectId);

        /// <summary>
        /// Updates an existing sprint.
        /// </summary>
        /// <param name="sprint">The sprint to update.</param>
        /// <returns>True if the sprint is updated successfully; otherwise, false.</returns>
        Task<bool> ProjectUpdateSprintAsync(ProjectSprint sprint);

        /// <summary>
        /// Deletes a sprint by ID.
        /// </summary>
        /// <param name="sprintId">The ID of the sprint to delete.</param>
        /// <returns>True if the sprint is deleted successfully; otherwise, false.</returns>
        Task<bool> ProjectDeleteSprintAsync(int sprintId);

        Task<List<ProjectSprint>> GetAllProjectSprintsByProjectIds(List<int> projectIds);
    }
}
