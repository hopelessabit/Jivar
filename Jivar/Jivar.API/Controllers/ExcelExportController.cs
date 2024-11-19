using Microsoft.AspNetCore.Mvc;
using Jivar.Service.Interfaces;
using Jivar.BO.Models;
using Jivar.Service.Util;
using Jivar.BO.Enumarate;

namespace Jivar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelExportController : ControllerBase
    {
        private readonly IExcelExportSerivce _excelExportSerivce;
        private readonly IProjectRoleService _projectRoleService;

        public ExcelExportController(IExcelExportSerivce excelExportSerivce, IProjectRoleService projectRoleService)
        {
            _excelExportSerivce = excelExportSerivce;
            _projectRoleService = projectRoleService;
        }
        [HttpGet("export-projects/{id}")]
        public async Task<IActionResult> ExportProjects(int id)
        {
            try
            {
                ProjectRole projectRole = await _projectRoleService.GetRoleByProjectIdAndAccountId(id, UserUtil.GetAccountId(HttpContext));
                if (projectRole == null || (!projectRole.Role.Equals(ProjectRoleType.Owner) && !projectRole.Role.Equals(ProjectRoleType.Owner)))
                    throw new Exception($"Can not find role for account's id: {UserUtil.GetAccountId(HttpContext)} in project with id: {id}");
            } catch (Exception e)
            {
                return Unauthorized(new Jivar.Service.PayLoads.ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = e.Message,
                    TimeStamp = DateTime.Now
                });
            }
            try
            {
                // Call the service to generate the Excel file as a byte array
                var fileContent = _excelExportSerivce.ExportProjectById(id);

                // Set the file name with timestamp for uniqueness
                var fileName = $"Projects_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                // Return the Excel file as a download
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error response
                // (Assuming a logging mechanism like Serilog or ILogger is set up)
                return StatusCode(500, new { Message = "An error occurred while exporting projects.", Details = ex.Message });
            }
        }
    }
}
