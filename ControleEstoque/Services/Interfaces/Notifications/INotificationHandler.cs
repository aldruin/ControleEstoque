using ControleEstoque.Models;

namespace ControleEstoque.Services.Interfaces.Notifications;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotification();
    bool AddNotification(Notification notification);
    bool AddNotification(string key, string message);
}