using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;


namespace AssociationBids.Portal.Service.Base.Code
{
    public class NotificationTemplateServices : BaseService, INotificationTemplateServices
    {

        protected INotificationTemplateRepository __oNotificationTemplateRepository;

        public NotificationTemplateServices()
         : this(new NotificationTemplateRepository()) { }

        public NotificationTemplateServices(string connectionString)
           : this(new NotificationTemplateRepository(connectionString)) { }

        public NotificationTemplateServices(INotificationTemplateRepository nOtificationTemRepository)
        {
            ConnectionString = nOtificationTemRepository.ConnectionString;

            __oNotificationTemplateRepository = nOtificationTemRepository;
        }

        public long NotificationEdit(NotificationTmpModel item)
        {
            return __oNotificationTemplateRepository.NotiFicationTmpEdit(item);
        }

        public long EmailTempletupdates(NotificationTmpModel item)
        {
            return __oNotificationTemplateRepository.NotiFicationTmpupdates(item);
        }

        public NotificationTmpModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<NotificationTmpModel> GetAll()
        {
            return __oNotificationTemplateRepository.GetAll();
        }

        public IList<LookUpModel> GetAllTitle()
        {
            return __oNotificationTemplateRepository.GetAllLookUp();
        }
        public IList<LookUpModel> GetAllTitleForNotification()
        {
            return __oNotificationTemplateRepository.GetAllLookUpForNotification();
        }

        public  virtual NotificationTmpModel GetDataViewEdit(int id)
        {
            return __oNotificationTemplateRepository.GetDataViewEdit(id);
        }

        public long Insert(NotificationTmpModel item)
        {
            return __oNotificationTemplateRepository.Insert(item);
        }

        public bool Remove(int id)
        {
            return __oNotificationTemplateRepository.Remove(id);
        }

        public List<NotificationTmpModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __oNotificationTemplateRepository.SearchUser(PageSize, PageIndex, Search, Sort);
        }

        public bool Validate(NotificationTmpModel item)
        {
            throw new NotImplementedException();
        }

        public long Notificationupdates(NotificationTmpModel item)
        {
            return __oNotificationTemplateRepository.NotiFicationTmpupdates(item);
        }

        public List<NotificationTmpModel> AdvancedSearchNotificationTemplate(long PageSize, long PageIndex, string Search, string TitleType, string Sort)
        {
            return __oNotificationTemplateRepository.AdvancedSearchNotificationTemplate(PageSize, PageIndex, Search, TitleType, Sort);
        }
    }
}
