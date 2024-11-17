using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jivar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost(APIEndPointConstant.DocumentE.UploadFile)]
        [ProducesResponseType(typeof(Document), StatusCodes.Status200OK)]
        public async Task<ActionResult> getSprintById(IFormFile file)
        {
            if (file == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _documentService.uploadFileAsync(file, int.Parse(userId));
            var response = new ApiResponse<Document>(StatusCodes.Status200OK, "Upload ảnh thành công thành công", result);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
