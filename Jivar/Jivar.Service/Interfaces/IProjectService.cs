﻿using Jivar.BO.Models;
using Jivar.Service.Paging;
using Jivar.Service.Payloads.Project.Request;
using Jivar.Service.Payloads.Project.Response;
using Microsoft.AspNetCore.Http;

namespace Jivar.Service.Interfaces
{
    public interface IProjectService
    {
        // Get a project by ID
        Task<Project> GetProjectById(int id);

        // Get a Project Response By ID
        Task<ProjectResponse> GetProjectResponseById(int id, bool? includeSprint = false, bool? includeRole = false, bool? includeTask = false);

        // Get all projects
        Task<PagedResult<ProjectResponse>> GetProjects(PagingAndSortingParams pagingParams, string? searchTerm = null, bool? includeSprint = false, bool? includeRole = false, bool? includeTask = false);

        // Create a new project
        Task<ProjectResponse> CreateProject(CreateProjectRequest request, HttpContext context);

        // Update an existing project
        Task<bool> UpdateProject(int projectId, UpdateProjectRequest request, HttpContext context);

        // Delete a project
        Task<bool> DeleteProject(int projectId);

        Task<PagedResult<ProjectResponse>> GetProjectByUserId(int userId, PagingAndSortingParams pagingParams, string? searchTerm = null, bool? includeSprint = false, bool? includeRole = false, bool? includeTask = false, bool? includeDocument = false);
    }
}
