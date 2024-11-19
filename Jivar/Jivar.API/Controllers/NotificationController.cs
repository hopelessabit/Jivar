using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost(APIEndPointConstant.NotificationE.NotificationEndpoint)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status200OK)]
        public void createNotification(int userId, string content)
        {
            _notificationService.createNotification(userId, content);
        }

        [HttpGet(APIEndPointConstant.NotificationE.GetNotificationEndpoint)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status200OK)]
        public async Task<ActionResult> getNotification(int accountId)
        {
            IEnumerable<Notification> notiResponse = _notificationService.getByAccId(accountId);
            var response = new ApiResponse<IEnumerable<Notification>>(StatusCodes.Status200OK, "Lấy danh sách notification thành công", notiResponse);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
