using Jivar.BO.Models;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.ProjectRole.Request;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectRoleController : ControllerBase
    {
        private readonly IProjectRoleService _projectRoleService;

        public ProjectRoleController(IProjectRoleService projectRoleService)
        {
            _projectRoleService = projectRoleService;
        }

        // GET: api/ProjectRole/project/{projectId}
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetRolesByProjectId(int projectId)
        {
            try
            {
                var roles = await _projectRoleService.GetRolesByProjectId(projectId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/ProjectRole/account/{accountId}
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetRolesByAccountId(int accountId)
        {
            try
            {
                var roles = await _projectRoleService.GetRolesByAccountId(accountId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/ProjectRole
        [HttpPost]
        public async Task<IActionResult> AddRoleToProject([FromBody] CreateProjectRoleRequest projectRole)
        {
            try
            {
                var success = await _projectRoleService.AddRoleToProject(projectRole, HttpContext);
                if (success)
                    return Ok(new { message = "Role added successfully." });
                return BadRequest(new { message = "Failed to add role to the project." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/ProjectRole
        [HttpPut]
        public async Task<IActionResult> UpdateProjectRole([FromBody] UpdateProjectRoleRequest request)
        {
            try
            {
                var success = await _projectRoleService.UpdateProjectRole(request, HttpContext);
                if (success)
                    return Ok(new { message = "Role updated successfully." });
                return BadRequest(new { message = "Failed to update role." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/ProjectRole/{projectId}/{accountId}
        [HttpDelete("{projectId}/{accountId}")]
        public async Task<IActionResult> RemoveRoleFromProject(int projectId, int accountId)
        {
            try
            {
                var success = await _projectRoleService.RemoveRoleFromProject(projectId, accountId, HttpContext);
                if (success)
                    return Ok(new { message = "Role removed successfully." });
                return BadRequest(new { message = "Failed to remove role from project." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/ProjectRole/role/{projectId}/{accountId}
        [HttpGet("role/{projectId}/{accountId}")]
        public async Task<IActionResult> GetRoleByProjectIdAndAccountId(int projectId, int accountId)
        {
            try
            {
                var role = await _projectRoleService.GetRoleByProjectIdAndAccountId(projectId, accountId);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
