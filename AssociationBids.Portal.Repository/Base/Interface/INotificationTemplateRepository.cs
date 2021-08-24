using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
   public interface INotificationTemplateRepository : IBaseRepository
    { 
        List<NotificationTmpModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort);
        NotificationTmpModel GetDataViewEdit(int id);
        //Int64 Insert(EmailTempletModel  item, string strinbuilder, string strinbuilder1);
        Int64 Insert(NotificationTmpModel item);
        IList<NotificationTmpModel> GetAll();
        IList<LookUpModel> GetAllLookUp();
        IList<LookUpModel> GetAllLookUpForNotification();
        Int64 NotiFicationTmpEdit(NotificationTmpModel item);
        Int64 NotiFicationTmpupdates(NotificationTmpModel item);
        //bool Delete(int id);
        bool Remove(int id);


        List<NotificationTmpModel> AdvancedSearchNotificationTemplate(long PageSize, long PageIndex, string Search, string TitleType, string Sort);
    }
}
