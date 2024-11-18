using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Enums;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Comment.Request;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Jivar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost(APIEndPointConstant.CommentE.CreateCommentEndpoint)]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
        public ActionResult createComment([Required] int taskId, [FromBody] CreateComment request)
        {
            if (request == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            Comment comment = new Comment
            {
                Content = request.content,
                TaskId = taskId,
                ParentId = request.parent_id,
                CreateBy = int.Parse(userId),
                CreateTime = DateTime.UtcNow,
                Status = CommentStatus.ACTIVE.ToString(),
            };
            var result = _commentService.createComment(comment);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Comment>(StatusCodes.Status200OK, "Comment thành công", result));
            }
            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<Comment>(StatusCodes.Status404NotFound, "Không tìm thấy taskId", null));
        }
    }
}
