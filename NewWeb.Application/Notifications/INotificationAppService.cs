using Abp.Application.Services;

namespace NewWeb.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        void NotificationUsersWhoHaveOpenTask();
    }
}
