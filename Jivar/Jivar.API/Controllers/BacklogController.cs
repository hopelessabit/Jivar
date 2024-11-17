using Jivar.Service.Implements;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Backlog.Request;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacklogController : ControllerBase
    {
        private readonly IBacklogService _backlogService;

        public BacklogController(IBacklogService backlogService)
        {
            _backlogService = backlogService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var backlog = await _backlogService.GetBacklogById(id);
                return Ok(backlog);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetByTaskId(int taskId)
        {
            var backlogs = await _backlogService.getBacklogByTaskId(taskId);
            return Ok(backlogs);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            var backlogs = await _backlogService.getBacklogByProjectId(projectId);
            return Ok(backlogs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBacklogRequest request)
        {
            try
            {
                var success = await _backlogService.createBacklog(request, HttpContext);
                if (success)
                    return Ok(new { message = "Backlog created successfully." });
                return BadRequest(new { message = "Failed to create backlog." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBacklogRequest request)
        {
            try
            {
                var success = await _backlogService.updateBacklog(id, request, HttpContext);
                if (success)
                    return Ok(new { message = "Backlog updated successfully." });
                return BadRequest(new { message = "Failed to update backlog." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _backlogService.deleteBacklog(id);
                if (success)
                    return Ok(new { message = "Backlog deleted successfully." });
                return BadRequest(new { message = "Failed to delete backlog." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
