using BusinessObjects.DTO.Notification;
using BusinessObjects.Models;
using BusinessObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Notifications
{
    public interface INotificationRepository
    {
        Task<Response> RemoveNotificationOlder();
        Task<IEnumerable<NotificationDTO>> GetNotificationsForUser(Guid userId);
        Task MarkAsRead();
    }
}
