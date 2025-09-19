using BusinessObjects.DTO.Notification;
using Microsoft.AspNetCore.Mvc;
using Repository.Notifications;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationsController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<NotificationDTO>> GetNotificationsForUser(Guid userId)
        {
            return await _notificationRepository.GetNotificationsForUser(userId);
        }

        [HttpPut]
        public async Task MarkAsRead()
        {
            await _notificationRepository.MarkAsRead();
        }
    }
}
