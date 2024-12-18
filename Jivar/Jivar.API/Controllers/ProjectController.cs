﻿using Jivar.Service.Constant;
using Jivar.Service.Interfaces;
using Jivar.Service.Paging;
using Jivar.Service.Payloads.Project.Request;
using Jivar.Service.Payloads.Project.Response;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // Get a project by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id,
            [FromQuery] bool? includeRole = null,
            [FromQuery] bool? includeSprint = null,
            [FromQuery] bool? includeTask = null)
        {
            try
            {
                var project = await _projectService.GetProjectResponseById(id, includeSprint, includeRole, includeTask);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] PagingAndSortingParams pagingParams,
            [FromQuery] bool? includeRole = null,
            [FromQuery] bool? includeSprint = null,
            [FromQuery] string? searchTerm = null,
            [FromQuery] bool? includeTask = null)
        {
            try
            {
                // Call the service to get paginated, sorted, and optionally filtered projects
                var result = await _projectService.GetProjects(pagingParams, searchTerm, includeSprint, includeRole, includeTask);

                // Return the result with an OK status
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a BadRequest status with the error message
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet(APIEndPointConstant.ProjectE.GetProjectByUserId)]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectsByUserId(
            [FromQuery] PagingAndSortingParams pagingParams,
            [FromQuery] string? searchTerm = null,
            [FromQuery] bool? includeRole = null,
            [FromQuery] bool? includeSprint = null,
            [FromQuery] bool? includeTask = null,
            [FromQuery] bool? includeDocument = null)
        {
            try
            {
                // Call the service to get paginated, sorted, and optionally filtered projects
                var result = await _projectService.GetProjectByUserId(UserUtil.GetAccountId(HttpContext), pagingParams, searchTerm, includeSprint, includeRole, includeTask, includeDocument);

                // Return the result with an OK status
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a BadRequest status with the error message
                return BadRequest(new { message = ex.Message });
            }
        }

        // Create a new project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                return Ok(await _projectService.CreateProject(request, HttpContext));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update an existing project
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectRequest request)
        {
            try
            {
                var success = await _projectService.UpdateProject(id, request, HttpContext);
                if (success)
                    return Ok(new { message = "Project updated successfully." });
                return BadRequest(new { message = "Failed to update project." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete a project
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var success = await _projectService.DeleteProject(id);
                if (success)
                    return Ok(new { message = "Project deleted successfully." });
                return BadRequest(new { message = "Failed to delete project." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
