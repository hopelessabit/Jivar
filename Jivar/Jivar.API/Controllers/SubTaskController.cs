using Jivar.BO.Models;
using Jivar.Service.Constant;
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

        [HttpPost(APIEndPointConstant.SubTaskE.CreateSubTask)]
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
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status200OK, "Không tìm thấy task ID", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<SubTask>(StatusCodes.Status200OK, "Tạo subTask thành công", resultOfAddSubTask));
        }

    }
}
