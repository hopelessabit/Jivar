using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Tasks.Request;
using Jivar.Service.Payloads.Tasks.Response;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly IDocumentService _documentService;
        private readonly ITaskDocumentService _taskDocumentService;

        public TaskController(ISprintTaskService sprintTaskService, IHttpContextAccessor context, ITaskService taskService, IDocumentService documentService, ITaskDocumentService taskDocumentService)
        {
            _sprintTaskService = sprintTaskService;
            _context = context;
            _taskService = taskService;
            _documentService = documentService;
            _taskDocumentService = taskDocumentService;
        }

        [HttpPost(APIEndPointConstant.TaskE.TaskEndpoint)]
        [ProducesResponseType(typeof(CreateTaskResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateNewTask(int sprintId, [FromBody] CreateTaskRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (request == null || sprintId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var task = new BO.Models.Task()
            {
                Title = request.Title,
                Description = request.Description,
                CreateBy = int.Parse(userId),
                CreateTime = DateTime.UtcNow,
                AssignBy = request.AssignBy,
                Assignee = request.Assignee,
                DocumentId = request.DocumentId,
                Status = TaskEnum.IN_PROGRESS.ToString(),
            };
            var result = _taskService.CreateTask(task);

            var documentTask = new TaskDocument(task.Id, request.DocumentId);
            _taskDocumentService.createTaskDocument(documentTask);
            var sprintTask = new BO.Models.SprintTask()
            {
                SprintId = sprintId,
                TaskId = result.Id,
                StartDate = request.startDateSprintTask,
                EndDate = request.endDateSprintTask,
            };
            await _sprintTaskService.AddSprintTask(sprintTask);
            var taskResponse = new CreateTaskResponse
            {
                Title = request.Title,
                Description = request.Description,
                CreateBy = task.CreateBy,
                CreateTime = DateTime.UtcNow,
                AssignBy = request.AssignBy,
                Assignee = request.Assignee,
                DocumentId = request.DocumentId,
                Status = task.Status,
            };
            var response = new ApiResponse<CreateTaskResponse>(StatusCodes.Status200OK, "Tạo task thành công", taskResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet(APIEndPointConstant.TaskE.TaskEndpoint)]
        [ProducesResponseType(typeof(IEnumerable<BO.Models.Task>), StatusCodes.Status200OK)]
        public async Task<ActionResult> getTasks()
        {
            IEnumerable<BO.Models.Task> taskResponse = _taskService.getTasks();
            var response = new ApiResponse<IEnumerable<BO.Models.Task>>(StatusCodes.Status200OK, "Lấy danh sách task thành công", taskResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet(APIEndPointConstant.TaskE.GetTaskById)]
        [ProducesResponseType(typeof(BO.Models.Task), StatusCodes.Status200OK)]
        public async Task<ActionResult> getTasksById(int id)
        {
            BO.Models.Task taskResponse = _taskService.getTasksById(id);
            if (taskResponse == null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<BO.Models.Task>(StatusCodes.Status200OK, "Task không tồn tại", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<BO.Models.Task>(StatusCodes.Status200OK, "Lấy task thành công", taskResponse));
        }

        [HttpDelete(APIEndPointConstant.TaskE.UpdateStatusTask)]
        [ProducesResponseType(typeof(BO.Models.Task), StatusCodes.Status200OK)]
        public async Task<ActionResult> getTasksById(int id, [Required] TaskEnum status)
        {
            BO.Models.Task taskResponse = _taskService.updateStatus(id, status.ToString());
            if (taskResponse == null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<BO.Models.Task>(StatusCodes.Status200OK, "Task không tồn tại", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<BO.Models.Task>(StatusCodes.Status200OK, "Cập nhật trạng thái task thành công", taskResponse));
        }

        [HttpPut(APIEndPointConstant.TaskE.GetTaskById)]
        [ProducesResponseType(typeof(BO.Models.Task), StatusCodes.Status200OK)]
        public async Task<ActionResult> updateTask([Required] int id, [FromBody] UpdateTaskRequest request)
        {
            BO.Models.Task task = _taskService.updateTask(id, request);
            if (task == null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<BO.Models.Task>(StatusCodes.Status200OK, "Task không tồn tại", null));
            }
            await _sprintTaskService.updateSprintTask(id, request.startDateSprintTask, request.endDateSprintTask);
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<BO.Models.Task>(StatusCodes.Status200OK, "Cập nhật thông tin task thành công", task));
        }
    }
}
