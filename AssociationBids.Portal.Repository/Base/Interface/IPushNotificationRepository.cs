using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IPushNotificationRepository : IBaseRepository
    {

        long Create(PushNotificationModel pushNotification);
        List<PushNotificationModel> PushNotification_SelectALL();
        List<PushNotificationModel> PushNotification_SelectByResourceKey(long ResourceKey);
        List<PushNotificationModel> PushNotification_SelectById(long Id);
        List<PushNotificationModel> PushNotification_SelectByType(long ResourceKey, string Type);

        List<string> GetUserTokensByUserKey(long UserKey);
        List<PushNotificationModel> SelectALLUnreadPushNotification(long UserKey);

        bool UpdateStatus(long PushNotificationKey, string MulticastId, string MessageId, string errorText);

        PushResponse SendPushNotificationById(long PushNotificationKey);

        void LoadPushNotification(PushNotificationJsonModel model);
    }
}
