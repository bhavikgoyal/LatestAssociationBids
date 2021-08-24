using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class AgreementService : BaseService, IAgreementService
    {
         protected IAgreementRepository __aGreementRepository;

        public AgreementService()
         : this(new AgreementRepository()) { }

        public AgreementService(string connectionString)
           : this(new AgreementRepository(connectionString)) { }

        public AgreementService(IAgreementRepository eMailTempletRepository)
        {
            ConnectionString = eMailTempletRepository.ConnectionString;

            __aGreementRepository = eMailTempletRepository;
        }

        public virtual bool Validate(AgreementModel item)
        {
            try
            {
                if (!Util.IsValidInt(item.PortalKey))
                {
                    AddError("PortalKey", "Portal can not be empty.");
                }
                if (!Util.IsValidText(item.Title))
                {
                    AddError("Title", "Title can not be empty.");
                }
                if (!Util.IsValidDateTime(item.AgreementDate))
                {
                    AddError("AgreementDate", "Agreement Date can not be empty.");
                }
                if (!Util.IsValidInt(item.Status))
                {
                    AddError("Status", "Status can not be empty.");
                }

                if (!IsValid)
                {
                    AddError("", "Error(s) encountered!");
                }

            }
            catch (Exception ex)
            {

                throw;
            }
           
            return IsValid;
        }

        public virtual bool IsFilterEnabled(AgreementFilterModel filter)
        {
            try
            {
                AgreementFilterModel defaultFilter = CreateFilter();

                if (defaultFilter.Status != filter.Status)
                    return true;
            }
            catch (Exception)
            {

                throw;
            }
          

            return false;
        }

        public virtual AgreementFilterModel CreateFilter()
        {
            try
            {
                AgreementFilterModel filter = new AgreementFilterModel();

                filter.PortalKey = SiteSettings.CurrentPortalKey;
                if (SiteSettings.AdminPortal)
                {
                    filter.Status = (int)LookUpType.RecordStatus.PendingApprovalOrApproved;
                }
                else
                {
                    filter.Status = (int)LookUpType.RecordStatus.Approved;
                }
                return UpdateFilter(filter);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public virtual AgreementFilterModel CreateFilter(AgreementModel item)
        {
            try
            {
                AgreementFilterModel filter = new AgreementFilterModel();

                filter.PortalKey = item.PortalKey;
                filter.Status = item.Status;

                return UpdateFilter(filter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public virtual AgreementFilterModel UpdateFilter(AgreementFilterModel filter)
        {
            return filter;
        }

        public virtual AgreementModel Insert()
        {
            ResetSiteSettings();

            AgreementModel item = new AgreementModel();

            item.PortalKey = SiteSettings.CurrentPortalKey;
            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public int Insert(AgreementModel item)
        {
            item.LastModificationTime = DateTime.Now;
            return __aGreementRepository.Insert(item);
        }
        public virtual bool Update(AgreementModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                UpdateSiteSettings(item.PortalKey);

                return __aGreementRepository.Update(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            return __aGreementRepository.Delete(id);
        }

        public virtual AgreementModel Get(int id)
        {
            AgreementModel item = __aGreementRepository.Get(id);

            UpdateSiteSettings(item.PortalKey);

            return item;
        }

        public virtual IList<AgreementModel> GetAll()
        {
            return __aGreementRepository.GetAll();
        }

        public virtual IList<AgreementModel> GetAll(AgreementFilterModel filter)
        {
            try
            {
                return __aGreementRepository.GetAll(UpdateFilter(filter));
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public virtual IList<AgreementModel> GetAll(AgreementFilterModel filter, PagingModel paging)
        {
            try
            {
                IList<AgreementModel> itemList = __aGreementRepository.GetAll(UpdateFilter(filter), paging);

                // Account for last record update on a page
                if (itemList.Count == 0 && paging.TotalRecordCount > 0)
                {
                    paging.PageNumber--;
                    return GetAll(filter, paging);
                }
                else
                {
                    return itemList;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public List<AgreementModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __aGreementRepository.SearchUser(PageSize, PageIndex, Search, Sort);
        }

        public AgreementModel GetDataViewEdit(int AgreementKey)
        {
            return __aGreementRepository.GetDataViewEdit(AgreementKey);
        }

        public long AgreementEdit(AgreementModel item)
        {
            return __aGreementRepository.AgreenemtEdit(item);
        }

        int IAgreementService.Insert(AgreementModel item)
        {
            return __aGreementRepository.Insert(item);
        }

        public List<AgreementModel> AdvancedSearchAgreement(long PageSize, long PageIndex, string Search, string Status, string Sort)
        {
            return __aGreementRepository.AdvancedSearchAgreement(PageSize, PageIndex, Search, Status, Sort);
        }
    }
}
