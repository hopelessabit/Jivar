using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface INotificationService
    {
        void createNotification(int userId, string content);
        IEnumerable<Notification> getByAccId(int accountId);
    }
}
