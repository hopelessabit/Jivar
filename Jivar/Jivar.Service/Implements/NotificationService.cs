using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly JivarDbContext _dbContext;

        public NotificationService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void createNotification(int userId, string content)
        {
            Notification notification = new Notification();
            notification.AccountId = userId;
            notification.Content = content;
            notification.CreateTime = DateTime.Now;
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Notification> getByAccId(int accountId)
        {
            return _dbContext.Notifications.Where(n => n.AccountId == accountId)
                 .OrderByDescending(n => n.CreateTime)
                .ToList();
        }
    }
}
