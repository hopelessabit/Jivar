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
        private readonly ITaskDocumentService _taskDocumentService;

        public DocumentController(IDocumentService documentService, ITaskDocumentService taskDocumentService)
        {
            _documentService = documentService;
            _taskDocumentService = taskDocumentService;
        }

        [HttpPost(APIEndPointConstant.DocumentE.UploadFile)]
        [ProducesResponseType(typeof(Document), StatusCodes.Status200OK)]
        public async Task<ActionResult> uploadFile(IFormFile file)
        {
            if (file == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _documentService.uploadFileAsync(file, int.Parse(userId));
            var response = new ApiResponse<Document>(StatusCodes.Status200OK, "Upload ảnh thành công", result);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet(APIEndPointConstant.DocumentE.DocumentEndpoint)]
        [ProducesResponseType(typeof(List<Document>), StatusCodes.Status200OK)]
        public ActionResult getDocuments()
        {
            var result = _documentService.getDocuments();
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<Document>>(StatusCodes.Status200OK, "Lấy documents thành công", result));
        }

        [HttpGet(APIEndPointConstant.DocumentE.GetDocumentById)]
        [ProducesResponseType(typeof(Document), StatusCodes.Status200OK)]
        public ActionResult getDocumentsById(int id)
        {
            var result = _documentService.getDocumentsById(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<Document>(StatusCodes.Status404NotFound, "Không tìm thấy documents", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<Document>(StatusCodes.Status200OK, "Lấy documents thành công", result));
        }

        [HttpDelete(APIEndPointConstant.DocumentE.GetDocumentById)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult deleteDocumentsById(int id)
        {
            var result = _documentService.deleteDocumentsById(id);
            if (result != true)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(StatusCodes.Status404NotFound, "Không tìm thấy documents", null));
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>(StatusCodes.Status200OK, "Xoá documents thành công", null));
        }
    }
}
