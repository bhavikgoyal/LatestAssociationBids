using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class PMPropertiesService : BaseService, IPMPropertiesService
    {
        protected IPMPropertiesRepository __pMPropertiesService;


        public PMPropertiesService()
           : this(new PMPropertiesRepository()) { }

        public PMPropertiesService(string connectionString)
           : this(new PMPropertiesRepository(connectionString)) { }

        public PMPropertiesService(PMPropertiesRepository pMPropertiesService)
        {
            ConnectionString = pMPropertiesService.ConnectionString;

            __pMPropertiesService = pMPropertiesService;
        }
        public virtual bool Validate(PropertyModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidInt(item.PropertyKey))
            {
                AddError("PropertyTypeKey", "Property Type can not be empty.");
            }
            if (!Util.IsValidInt(item.Status))
            {
                AddError("Status", "Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }
        public virtual bool IsFilterEnabled(PropertyFilterModel filter)
        {
            PropertyFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            if (defaultFilter.PropertyKeyList != filter.PropertyKeyList)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;


            return false;
        }
        public virtual PropertyFilterModel CreateFilter()
        {
            PropertyFilterModel filter = new PropertyFilterModel();

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
        public virtual IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging)
        {
            IList<PropertyModel> itemList = __pMPropertiesService.GetAll(UpdateFilter(filter), paging);

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

        public List<PropertyModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort, int ResourceKey)
        {
            return __pMPropertiesService.SearchUser(PageSize, PageIndex, Search, Sort, ResourceKey);
        }

        public List<PropertyModel> SearchBidrequest(long PageSize, long PageIndex, string Sort, string PropertyKey)
        {
            return __pMPropertiesService.SearchBidrequest(PageSize, PageIndex, Sort, PropertyKey);
        }

        public List<PropertyModel> SearchWorkOrder(long PageSize, long PageIndex, string Sort, string PropertyKey)
        {
            return __pMPropertiesService.SearchWorkOrder(PageSize, PageIndex, Sort, PropertyKey);
        }

        public IList<BidRequestModel> GetbindDocumentBid(int CompanyKey, int ModuleKey)
        {
            return __pMPropertiesService.GetbindDocumentBid(CompanyKey, ModuleKey);
        }

        public PropertyModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<PropertyModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<PropertyModel> GetAll(PropertyFilterModel filter)
        {
            throw new NotImplementedException();
        }
        public virtual PropertyModel GetDataViewEdit(int id)
        {
            return __pMPropertiesService.GetDataViewEdit(id);
        }


        public List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            return __pMPropertiesService.SearchVendorByBidRequest(BidRequestKey, modulekey);
        }
        public List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, int Resourcekey)
        {
            return __pMPropertiesService.SearchVendorByBidRequest(BidRequestKey, modulekey, Resourcekey);
        }
        
        public virtual PropertyModel GetBidDataForProperties(int BidRequestKey)
        {
            return __pMPropertiesService.GetBidDataForProperties(BidRequestKey);
        }

        //public Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1)
        //{
        //    return __pMPropertiesService.Insert(item, strinbuilder, strinbuilder1);
        //}

        public Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1)
        {
            return __pMPropertiesService.Insert(item, strinbuilder, strinbuilder1);
        }

        public virtual bool DocInsert(int PropertyKey, string strinbuilder, string strinbuilder1)
        {
            return __pMPropertiesService.DocInsert(PropertyKey, strinbuilder, strinbuilder1);
        }

        public int GetGroupKey(string managername)
        {
            return __pMPropertiesService.GetGroupKey(managername);
        }



        public virtual bool Updatemanager(PropertyModel item, int PropertyKey, string managername)
        {
            return __pMPropertiesService.Updatemanager(item, PropertyKey, managername);
        }



        public IList<PropertyModel> GetAllManager(int ResourceId, int PropertyKey, int CompnyKey)
        {
            return __pMPropertiesService.GetAllManager(ResourceId, PropertyKey, CompnyKey);
        }


        public IList<PropertyModel> GetPropertyManagerToAddProperty(int ResourceId, int CompnyKey)
        {
            return __pMPropertiesService.GetPropertyManagerToAddProperty(ResourceId, CompnyKey);
        }

        public IList<PropertyModel> GetManagerForAdd(int ResourceId, int PropertyKey, int CompnyKey)
        {
            return __pMPropertiesService.GetManagerForAdd(ResourceId, PropertyKey, CompnyKey);
        }


        public IList<PropertyModel> GetAllCompany()
        {
            return __pMPropertiesService.GetAllCompany();
        }


        public IList<PropertyModel> GetAllState()
        {
            return __pMPropertiesService.GetAllState();
        }

        public virtual bool PropertyEdit(PropertyModel item)
        {

            return __pMPropertiesService.PropertyEdit(item);

        }
        public IList<PropertyModel> GetbindDocument(int PropertyKey)
        {
            return __pMPropertiesService.GetbindDocument(PropertyKey);
        }






        public virtual bool DocumentDelete(int PropertyKey, string Docname)
        {

            return __pMPropertiesService.DocumentDelete(PropertyKey, Docname);

        }

        public virtual bool ManagerDelete(int PropertyKey, string ResourceKey)
        {

            return __pMPropertiesService.ManagerDelete(PropertyKey, ResourceKey);

        }

        public PropertyModel checkmanager(string managername)
        {

            return __pMPropertiesService.checkmanager(managername);

        }



        public virtual bool Remove(int id)
        {

            return __pMPropertiesService.Remove(id);

        }
        public IList<PropertyModel> GetAllManager()
        {
            throw new NotImplementedException();
        }
    }


}
