using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class ABNotificationService : BaseService, IABNotificationService
    {
        protected IABNotificationRepository __notificationService;

        public ABNotificationService()
         : this(new ABNotificationRepository()) { }

        public ABNotificationService(string connectionString)
           : this(new ABNotificationRepository(connectionString)) { }

        public ABNotificationService(ABNotificationRepository notificationRepository)
        {
            ConnectionString = notificationRepository.ConnectionString;

            __notificationService = notificationRepository;


        }

        public bool InsertNotification(string NotificationType, int ModuleKey, long ObjectKey, long ByResourceKey, string Text,long ForResourceKey = 0)
        {
            return __notificationService.InsertNotification(NotificationType, ModuleKey, ObjectKey, ByResourceKey, Text,ForResourceKey);
        }
        public bool InsertNotificationdashborad(string NotificationType, int ModuleKey, long ObjectKey, long ByResourceKey, string Text, long ForResourceKey = 0)
        {
            return __notificationService.InsertNotificationdashborad(NotificationType, ModuleKey, ObjectKey, ByResourceKey, Text, ForResourceKey);
        }
        public List<ABNotificationModel> GetABNotificationsFive(long ResourceKey)
        {
            return __notificationService.GetABNotificationsFive(ResourceKey);
        }

        public List<ABNotificationModel> GetABNotificationsAll(long ResourceKey)
        {
            return __notificationService.GetABNotificationsAll(ResourceKey);
        }
        public string GetFiveNotificationsForVendorFiveNew(long ResourceKey)
        {
            return __notificationService.GetFiveNotificationsForVendorFiveNew(ResourceKey);
        }
        public List<ABNotificationModel> GetABNotificationsAllByModuleAndType(long ResourceKey, int ModuleKey, string NotificationType)
        {
            return __notificationService.GetABNotificationsAllByModuleAndType(ResourceKey,ModuleKey,NotificationType);
        }
        public List<ABNotificationModel> GetABNotificationsAllByObjectAndModule(long ResourceKey, int ModuleKey, long ObjectKey)
        {
            return __notificationService.GetABNotificationsAllByObjectAndModule(ResourceKey, ModuleKey, ObjectKey);
        }
        public List<ABNotificationModel> GetABNotificationsModule(long ResourceKey)
        {
            return __notificationService.GetABNotificationsModule(ResourceKey);
        }
        public ABNotificationModel GetABNotificationByNotificationId(long ResourceKey, long NotificationId)
        {
            return __notificationService.GetABNotificationByNotificationId(ResourceKey, NotificationId);
        }
        public bool UpdateStatus(long NotificationId, string Status)
        {
            return __notificationService.UpdateStatus(NotificationId, Status);
        }
        public bool UpdateStatusByObjectKey(long ObjectKey, string Status, string NotiType)
        {
            return __notificationService.UpdateStatusByObjectKey(ObjectKey, Status, NotiType);
        }
        public bool UpdateStatsVendordash(long NotificationId, string Status, int Objectkey)
        {
            return __notificationService.UpdateStatsVendordash(NotificationId, Status, Objectkey);
        }
        public string GetFiveNotificationsForManagerFiveNew(long ResourceKey, int BIDSUBMITDAYS)
        {
            return __notificationService.GetFiveNotificationsForManagerFiveNew(ResourceKey, BIDSUBMITDAYS);
        }
        public string RequestSendOrNot(long ResourceKey, string BidName)
        {
            return __notificationService.RequestSendOrNot(ResourceKey, BidName);
        }
    }
}
