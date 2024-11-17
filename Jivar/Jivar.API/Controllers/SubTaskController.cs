using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.SubTask.Request;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Jivar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService _subTaskService;
        private readonly IGroupTaskService _groupTaskService;

        public SubTaskController(ISubTaskService subTaskService, IGroupTaskService groupTaskService)
        {
            _subTaskService = subTaskService;
            _groupTaskService = groupTaskService;
        }

        [HttpPost(APIEndPointConstant.SubTaskE.SubTaskEndpoint)]
        [ProducesResponseType(typeof(SubTask), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateNewSubTaskAsync([Required] int taskId, [FromBody] CreateSubTaskRequest request)
        {
            if (request == null || taskId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var resultOfAddSubTask = await _subTaskService.createSubTask(request);
            var resultOfAddGroupTask = await _groupTaskService.createGroupTask(taskId, resultOfAddSubTask.Id);
            if (resultOfAddGroupTask == false)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status404NotFound, "Không tìm thấy task ID", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status200OK, "Tạo subTask thành công", resultOfAddSubTask));
        }

        [HttpGet(APIEndPointConstant.SubTaskE.SubTaskEndpoint)]
        [ProducesResponseType(typeof(List<SubTask>), StatusCodes.Status200OK)]
        public async Task<ActionResult> listSubTask()
        {
            var resultOfSubTask = await _subTaskService.listSubTasks();
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<SubTask>>(StatusCodes.Status200OK, "Lấy danh sách subtask thành công", resultOfSubTask));
        }

        [HttpGet(APIEndPointConstant.SubTaskE.GetTaskById)]
        [ProducesResponseType(typeof(SubTask), StatusCodes.Status200OK)]
        public async Task<ActionResult> listSubTaskByTaskId(int id)
        {
            var resultOfSubTask = await _subTaskService.listSubTaskByTaskId(id);
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status200OK, "Lấy danh sách subtask thành công", resultOfSubTask));
        }

        [HttpGet(APIEndPointConstant.SubTaskE.GetTaskByIV2)]
        [ProducesResponseType(typeof(IEnumerable<SubTask>), StatusCodes.Status200OK)]
        public async Task<ActionResult> listSubTaskByTaskIdV2(int taskId)
        {
            var resultOfSubTask = await _subTaskService.listGroupTask(taskId);
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<SubTask>>(StatusCodes.Status200OK, "Lấy danh sách subtask thành công", resultOfSubTask));
        }

        [HttpPut(APIEndPointConstant.SubTaskE.GetTaskById)]
        [ProducesResponseType(typeof(SubTask), StatusCodes.Status200OK)]
        public ActionResult updateSubTask([Required] int id, [FromBody] UpdateSubTask request, [Required] SubTaskEnum status)
        {
            var resultOfSubTask = _subTaskService.updateSubTask(id, request, status.ToString());
            if (resultOfSubTask == null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status404NotFound, "Không tìm thấy subTask ID", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status200OK, "Cập nhật subtask thành công", resultOfSubTask));
        }

        [HttpDelete(APIEndPointConstant.SubTaskE.GetTaskById)]
        [ProducesResponseType(typeof(SubTask), StatusCodes.Status200OK)]
        public ActionResult deleteSubTask([Required] int id)
        {
            bool resultOfSubTask = _groupTaskService.deleteGroupTask(id);
            if (!resultOfSubTask)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status404NotFound, "Không tìm thấy subTask ID", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status200OK, "Xoá subTask thành công", null));
        }
    }
}
