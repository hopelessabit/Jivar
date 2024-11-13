using Jivar.Service.Constant;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Task.Request;
using Jivar.Service.Payloads.Task.Response;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jivar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ISprintTaskService _sprintTaskService;
        private readonly ITaskService _taskService;
        private readonly IHttpContextAccessor _context;

        public TaskController(ISprintTaskService sprintTaskService, IHttpContextAccessor context, ITaskService taskService)
        {
            _sprintTaskService = sprintTaskService;
            _context = context;
            _taskService = taskService;
        }

        [HttpPost(APIEndPointConstant.TaskE.TaskEndpoint)]
        [ProducesResponseType(typeof(CreateTaskResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateNewSprint([FromBody] CreateTaskRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (request == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var task = new BO.Models.Task()
            {
                Title = request.Title,
                Description = request.Description,
                CreateBy = Int32.Parse(userId),
                CreateTime = DateTime.UtcNow,
                AssignBy = request.AssignBy,
                Assignee = request.Assignee,
                CompleteTime = request.CompleteTime,
                DocumentId = request.DocumentId,
                Status = TaskEnum.IN_PROGRESS.ToString(),
            };
            var result = await _taskService.CreateTask(task);

            var taskResponse = new CreateTaskResponse
            {
                Title = request.Title,
                Description = request.Description,
                CreateBy = task.CreateBy,
                CreateTime = DateTime.UtcNow,
                AssignBy = request.AssignBy,
                Assignee = request.Assignee,
                CompleteTime = request.CompleteTime,
                DocumentId = request.DocumentId,
                Status = task.Status,
            };
            var response = new ApiResponse<CreateTaskResponse>(StatusCodes.Status200OK, "Tạo task thành công", taskResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
