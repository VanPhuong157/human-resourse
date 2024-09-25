using BusinessObjects.DTO.Notification;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.NotificationsDAO;
using DataAccess.Okrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDAO _notificationDAO;
        public NotificationRepository(NotificationDAO notificationDAO)
        {
            _notificationDAO = notificationDAO;
        }
        public Task<Response> RemoveNotificationOlder()
        {
            return _notificationDAO.RemoveNotificationOlder();
        }
        public async Task<IEnumerable<NotificationDTO>> GetNotificationsForUser(Guid userId)
        {
            return await _notificationDAO.GetNotificationsForUser(userId);
        }
        public async Task MarkAsRead()
        {
             await _notificationDAO.MarkAllAsRead();
        }
    }
}
