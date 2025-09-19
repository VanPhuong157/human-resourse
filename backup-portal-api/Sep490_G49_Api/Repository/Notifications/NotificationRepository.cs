using AutoMapper;
using BusinessObjects.DTO.Notification;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.EntityFrameworkCore;

namespace Repository.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;

        public NotificationRepository(SEP490_G49Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> RemoveNotificationOlder()
        {
            DateTime currentDate = DateTime.UtcNow.AddHours(7);
            DateTime dateThreshold = currentDate.AddDays(-3);
            List<Notification> notificationsOlder = _context.Notifications
                .Where(x => x.CreatedAt < dateThreshold)
                .ToList();
            _context.Notifications.RemoveRange(notificationsOlder);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success };
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsForUser(Guid userId)
        {
            var notification = await _context.Notifications
                .Include(x => x.User)
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return _mapper.Map<IEnumerable<NotificationDTO>>(notification);
        }

        public async Task MarkAsRead()
        {
            var notifications = await _context.Notifications.ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
