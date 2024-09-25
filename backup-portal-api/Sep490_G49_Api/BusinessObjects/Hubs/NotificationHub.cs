using BusinessObjects.Models;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotification(Notification notification)
    {
        await Clients.All.SendAsync("ReceiveNotification", notification.UserId, new
        {
            CreateAt = notification.CreatedAt,
            Message = notification.Message,
            RedirectUrl = notification.RedirectUrl
        });

    }
}