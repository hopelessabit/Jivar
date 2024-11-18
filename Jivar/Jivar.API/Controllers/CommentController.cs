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

        [HttpPost(APIEndPointConstant.CommentE.GetCommentEndpoint)]
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

        [HttpGet(APIEndPointConstant.CommentE.GetCommentEndpoint)]
        [ProducesResponseType(typeof(IEnumerable<Comment>), StatusCodes.Status200OK)]
        public ActionResult getCommentByTaskId([Required] int taskId)
        {
            var result = _commentService.getCommentByTaskId(taskId);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<Comment>>(StatusCodes.Status200OK, "Comment thành công", result));
            }
            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<IEnumerable<Comment>>(StatusCodes.Status404NotFound, "Không tìm thấy comment by taskId", null));
        }


        [HttpDelete(APIEndPointConstant.CommentE.GetCommentByIdEndpoint)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult getCommentById([Required] int id)
        {
            bool result = _commentService.getCommentById(id);
            if (result != false)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>(StatusCodes.Status200OK, "Xoá Comment thành công", null));
            }
            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(StatusCodes.Status404NotFound, "Không tìm thấy comment", null));
        }

        [HttpPut(APIEndPointConstant.CommentE.GetCommentByIdEndpoint)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult updateCommentById(int id, [FromBody] UpdateCommentRequest request)
        {
            bool result = _commentService.updateCommentById(id, request);
            if (result != false)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>(StatusCodes.Status200OK, "Cập nhật Comment thành công", null));
            }
            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(StatusCodes.Status404NotFound, "Không tìm thấy comment", null));
        }

        [HttpPost(APIEndPointConstant.CommentE.ReplyCommentByIdEndpoint)]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
        public ActionResult replyComment([Required] int commentId, [FromBody] ReplayComment request)
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
            var result = _commentService.createReplayComment(commentId, request, int.Parse(userId));
            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Comment>(StatusCodes.Status200OK, "Reply Comment thành công", result));
            }
            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<Comment>(StatusCodes.Status404NotFound, "Không tìm thấy commentId", null));
        }
    }
}
