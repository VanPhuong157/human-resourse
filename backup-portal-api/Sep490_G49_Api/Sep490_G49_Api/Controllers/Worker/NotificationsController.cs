using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Notifications;
using Repository.Objectives;

namespace Sep490_G49_Api.Controllers.Worker
{
    [Route("api/worker/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationsController(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        [HttpGet]
        public Task<Response> RemoveNotificationOlder()
        {
            Task<Response> response = notificationRepository.RemoveNotificationOlder();
            return response;
        }
    }
}
