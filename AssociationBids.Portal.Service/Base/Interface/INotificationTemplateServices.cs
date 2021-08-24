using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface INotificationTemplateServices : IBaseService
    {


        //List<EmailTemplateModel> SearchUser(string Search);
        List<NotificationTmpModel > SearchUser(long PageSize, long PageIndex, string Search, string Sort);
        bool Validate(NotificationTmpModel item);
        NotificationTmpModel Get(int id);
        IList<NotificationTmpModel> GetAll();
        Int64 Insert(NotificationTmpModel item);
        NotificationTmpModel GetDataViewEdit(int id);
        IList<LookUpModel> GetAllTitle();
        IList<LookUpModel> GetAllTitleForNotification();
        
        Int64 NotificationEdit(NotificationTmpModel item);
        Int64 Notificationupdates(NotificationTmpModel item);
        bool Remove(int id);

        List<NotificationTmpModel> AdvancedSearchNotificationTemplate(long PageSize, long PageIndex, string Search, string TitleType, string Sort);
    }
    
}