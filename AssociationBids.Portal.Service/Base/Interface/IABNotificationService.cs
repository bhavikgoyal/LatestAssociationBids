using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IABNotificationService : IBaseService
    {
        bool InsertNotification(string NotificationType, int ModuleKey, long ObjectKey, long ByResourceKey, string Text,long ForResourceKey = 0);
        bool InsertNotificationdashborad(string NotificationType, int ModuleKey, long ObjectKey, long ByResourceKey, string Text, long ForResourceKey = 0);

        List<ABNotificationModel> GetABNotificationsFive(long ResourceKey);

        List<ABNotificationModel> GetABNotificationsAll(long ResourceKey);

        string GetFiveNotificationsForVendorFiveNew(long ResourceKey);

        List<ABNotificationModel> GetABNotificationsAllByModuleAndType(long ResourceKey, int ModuleKey, string NotificationType);

        List<ABNotificationModel> GetABNotificationsAllByObjectAndModule(long ResourceKey, int ModuleKey, long ObjectKey);

        List<ABNotificationModel> GetABNotificationsModule(long ResourceKey);

        ABNotificationModel GetABNotificationByNotificationId(long ResourceKey, long NotificationId);
        bool UpdateStatsVendordash(long NotificationId, string Status, int Objectkey);
        bool UpdateStatus(long NotificationId, string Status);
        bool UpdateStatusByObjectKey(long ObjectKey, string Status, string NotiType);
        string GetFiveNotificationsForManagerFiveNew(long ResourceKey, int BIDSUBMITDAYS);
        string RequestSendOrNot(long ResourceKey, string BidName);
    }
}
