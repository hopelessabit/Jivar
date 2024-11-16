using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Sprint.Request;
using Jivar.Service.Payloads.Sprint.Response;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;
        private readonly ISprintTaskService _sprintTaskService;

        public SprintController(ISprintService sprintService, ISprintTaskService sprintTaskService)
        {
            _sprintService = sprintService;
            _sprintTaskService = sprintTaskService;
        }

        [HttpPost(APIEndPointConstant.SprintE.CreateSprint)]
        [ProducesResponseType(typeof(CreateSprintResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateNewSprint(int projectId, [FromBody] CreateSprintRequest request)
        {
            if (request == null || projectId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var sprint = new Sprint()
            {
                ProjectId = projectId,
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            var result = await _sprintService.createSprint(sprint);

            var SprintResponse = new CreateSprintResponse
            {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
            };
            var response = new ApiResponse<CreateSprintResponse>(StatusCodes.Status200OK, "Tạo sprint thành công", SprintResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }



        [HttpGet(APIEndPointConstant.SprintE.SprintEndpoint)]
        [ProducesResponseType(typeof(IEnumerable<CreateSprintResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> listSprint()
        {
            var result = await _sprintService.listSprints();
            var sprintResponses = result.Select(sprint => new CreateSprintResponse
            {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
            }).ToList();
            var response = new ApiResponse<IEnumerable<CreateSprintResponse>>(StatusCodes.Status200OK, "Lấy sprint thành công", sprintResponses);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet(APIEndPointConstant.SprintE.GetSprintById)]
        [ProducesResponseType(typeof(CreateSprintResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> getSprintById(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var result = await _sprintService.getSprintById(id);
            var SprintResponse = new CreateSprintResponse
            {
                Id = result.Id,
                ProjectId = result.ProjectId,
                Name = result.Name,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
            };
            var response = new ApiResponse<CreateSprintResponse>(StatusCodes.Status200OK, "Lấy sprint thành công", SprintResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut(APIEndPointConstant.SprintE.UpdateSprint)]
        [ProducesResponseType(typeof(CreateSprintResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> updateSprint(int? id, [FromBody] UpdateSprintResponse request)
        {
            if (request == null || id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var result = await _sprintService.getSprintById(id);
            if (result == null)
            {

                return StatusCode(StatusCodes.Status404NotFound);
            }
            result.Name = request.Name ?? result.Name;
            result.StartDate = request.StartDate ?? result.StartDate;
            result.EndDate = request.EndDate ?? result.EndDate;
            var isUpdated = await _sprintService.updateSprint(result);
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);  // Handle update failure
            }
            var sprintResponse = new CreateSprintResponse
            {
                Id = result.Id,
                ProjectId = result.ProjectId,
                Name = result.Name,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
            };
            var response = new ApiResponse<CreateSprintResponse>(StatusCodes.Status200OK, "Cập nhật sprint thành công", sprintResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete(APIEndPointConstant.SprintE.UpdateSprint)]
        [ProducesResponseType(typeof(CreateSprintResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> deleteSprint(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var result = await _sprintService.getSprintById(id);
            if (result == null)
            {

                return StatusCode(StatusCodes.Status404NotFound);
            }
            var resultOfDeleteSprintTask = await _sprintTaskService.deleteSprintTask(id);
            var resultOfDeleteSprint = await _sprintService.deleteSprint(result);
            var sprintResponse = new CreateSprintResponse
            {
                Id = result.Id,
                ProjectId = result.ProjectId,
                Name = result.Name,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
            };
            var response = new ApiResponse<CreateSprintResponse>(StatusCodes.Status200OK, "Xoá sprint thành công", sprintResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }

    }
}
