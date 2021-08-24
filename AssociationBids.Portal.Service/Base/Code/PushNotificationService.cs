using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Repository.Base.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Service.Base.Interface;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class PushNotificationService : BaseService, IPushNotificationService
    {
        protected IPushNotificationRepository __pushnotificationService;

        public PushNotificationService()
         : this(new PushNotificationRepository()) { }

        public PushNotificationService(string connectionString)
           : this(new PushNotificationRepository(connectionString)) { }

        public PushNotificationService(PushNotificationRepository pushnotificationRepository)
        {
            ConnectionString = pushnotificationRepository.ConnectionString;

            __pushnotificationService = pushnotificationRepository;
        }


        public long Create(PushNotificationModel pushNotification)
        {
            return __pushnotificationService.Create(pushNotification);
        }
        public List<PushNotificationModel> PushNotification_SelectALL()
        {
            return __pushnotificationService.PushNotification_SelectALL();
        }
        public List<PushNotificationModel> PushNotification_SelectByResourceKey(long ResourceKey)
        {
            return __pushnotificationService.PushNotification_SelectByResourceKey(ResourceKey);
        }
        public List<PushNotificationModel> PushNotification_SelectById(long Id)
        {
            return __pushnotificationService.PushNotification_SelectById(Id);
        }
        public List<PushNotificationModel> PushNotification_SelectByType(long ResourceKey, string Type)
        {
            return __pushnotificationService.PushNotification_SelectByType(ResourceKey, Type);
        }

        public List<string> GetUserTokensByUserKey(long UserKey)
        {
            return __pushnotificationService.GetUserTokensByUserKey(UserKey);
        }

        public List<PushNotificationModel> SelectALLUnreadPushNotification(long UserKey)
        {
            return __pushnotificationService.SelectALLUnreadPushNotification(UserKey);
        }
        public bool UpdateStatus(long PushNotificationKey, string MulticastId, string MessageId, string errorText)
        {
            return __pushnotificationService.UpdateStatus(PushNotificationKey, MulticastId, MessageId, errorText);
        }

        public PushResponse SendPushNotificationById(long PushNotificationKey)
        {
            return __pushnotificationService.SendPushNotificationById(PushNotificationKey);
        }
        public void LoadPushNotification(PushNotificationJsonModel model)
        {
            __pushnotificationService.LoadPushNotification(model);
        }
    }
}
