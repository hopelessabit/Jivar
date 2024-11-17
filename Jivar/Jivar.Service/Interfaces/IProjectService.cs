using Jivar.BO.Models;
using Jivar.Service.Paging;
using Jivar.Service.Payloads.Project.Request;
using Jivar.Service.Payloads.Project.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Interfaces
{
    public interface IProjectService
    {
        // Get a project by ID
        Task<Project> GetProjectById(int id);

        // Get a Project Response By ID
        Task<ProjectResponse> GetProjectResponseById(int id);

        // Get all projects
        Task<PagedResult<ProjectResponse>> GetProjects(PagingAndSortingParams pagingParams, string? searchTerm = null, bool? includeSprint = false, bool? includeRole = false);

        // Create a new project
        Task<ProjectResponse> CreateProject(CreateProjectRequest request, HttpContext context);

        // Update an existing project
        Task<bool> UpdateProject(int projectId, UpdateProjectRequest request, HttpContext context);

        // Delete a project
        Task<bool> DeleteProject(int projectId);
    }
}
