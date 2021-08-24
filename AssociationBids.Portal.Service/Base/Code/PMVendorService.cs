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
    public class PMVendorService : BaseService, IPMVendorService
    {
        protected IPMVendorRepository __vendorservice;

        public PMVendorService()
         : this(new PMVendorRepository()) { }

        public PMVendorService(string connectionString)
           : this(new PMVendorRepository(connectionString)) { }

        public PMVendorService(PMVendorRepository vendorRepository)
        {
            ConnectionString = vendorRepository.ConnectionString;

            __vendorservice = vendorRepository;

           
    }

        public virtual bool Validate(VendorModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidInt(item.ServiceKey))
            {
                AddError("ServiceKey", "Property Type can not be empty.");
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

        public virtual bool IsFilterEnabled(VendorFilterModel filter)
        {
            VendorFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;


            return false;
        }

        public virtual VendorFilterModel CreateFilter()
        {
            VendorFilterModel filter = new VendorFilterModel();

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



        public List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            return __vendorservice.SearchVendorByBidRequest(BidRequestKey, modulekey);
        }
        public List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, int resourcekey)
        {
            return __vendorservice.SearchVendorByBidRequest(BidRequestKey, modulekey, resourcekey);
        }


        public virtual VendorModel GetBidDataForProperties(int BidRequestKey, int ComapanyKey)
        {
            return __vendorservice.GetBidDataForProperties(BidRequestKey, ComapanyKey);
        }

        public List<VendorModel> SearchVendor(long PageSize, long PageIndex, string Search, string Sort, int ResourceKey, string service, string checkstar, string Invited,string Duplicate) 
        {
            return __vendorservice.SearchVendor(PageSize, PageIndex, Search, Sort, ResourceKey, service, checkstar, Invited, Duplicate);
        }

        public List<VendorModel> SearchBidrequest(long PageSize, long PageIndex, string Sort,string CompanyKey)
        {
            return __vendorservice.SearchBidrequest(PageSize, PageIndex, Sort,CompanyKey);
        }

        public List<VendorModel> SearchWorkOrder(long PageSize, long PageIndex, string Sort, string CompanyKey)
        {
            return __vendorservice.SearchWorkOrder(PageSize, PageIndex, Sort, CompanyKey);
        }

        public List<VendorModel> SearchFeedbackvendor(long PageSize, long PageIndex, string Sort,string CompanyKey)
        {
            return __vendorservice.SearchFeedbackvendor(PageSize, PageIndex, Sort,CompanyKey);
        }


        public List<VendorModel> Searchinsurance(int CompanyKey)
        {
            return __vendorservice.Searchinsurance(CompanyKey);
        }

        public virtual IList<VendorModel> GetAll(VendorFilterModel filter, PagingModel paging)
        {
            IList<VendorModel> itemList = __vendorservice.GetAll(UpdateFilter(filter), paging);

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

        public Int64 Insert(VendorModel vendorModel, int ResourceKey)
        {
            return __vendorservice.Insert(vendorModel, ResourceKey);
        }

        public IList<VendorModel> Getbindservice(int CompanyKey)
        {
            return __vendorservice.Getbindservice(CompanyKey);
        }


        public IList<VendorModel> GetbindDocument(int CompanyKey, int ModuleKey)
        {
            return __vendorservice.GetbindDocument(CompanyKey, ModuleKey);
        }
        public IList<VendorModel> GetbindDocument1(int CompanyKey, int ModuleKey)
        {
            return __vendorservice.GetbindDocument(CompanyKey, ModuleKey);
        }

        public IList<VendorModel> GetbindDocumentByCompanyKey(int CompanyKey)
        {
            return __vendorservice.GetbindDocumentByCompanyKey(CompanyKey);
        }

        public IList<PropertyModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<VendorModel> GetAllState()
        {
            return __vendorservice.GetAllState();
        }

        public IList<VendorModel> GetAllService()
        {
            return __vendorservice.GetAllService();
        }

        public IList<VendorModel> GetAllProperty(int ResourceKey)
        {
            return __vendorservice.GetAllProperty(ResourceKey);
        }

        public VendorModel Get(int id)
        {
            throw new NotImplementedException();
        }

        IList<VendorModel> IPMVendorService.GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<VendorModel> GetAll(VendorFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public virtual VendorModel GetDataViewEdit(int id)
        {
            return __vendorservice.GetDataViewEdit(id);
        }

        public Int64 VendorEdit(VendorModel item)
        {

            return __vendorservice.VendorEdit(item);

        }


        public bool CheckDuplicatedEmail(string Email)
        {
            return __vendorservice.CheckDuplicatedEmail(Email);
        }
        public virtual bool insuranceEdit(VendorModel item)
        {

            return __vendorservice.insuranceEdit(item);

        }


        public bool Remove(int CompanyKey)
        {
            return __vendorservice.Remove(CompanyKey);
        }


        public bool RemoveService(int CompanyKey,string servicename)
        {
            return __vendorservice.RemoveService(CompanyKey,servicename);
        }

        public bool RemoveDocument(int CompanyKey, int docId)
        {
            return __vendorservice.RemoveDocument(CompanyKey, docId);
        }

        public bool MarkstarOrNot(int CompanyKey, int Resourcekey)
        {
            return __vendorservice.MarkstarOrNot(CompanyKey, Resourcekey);
        }

        public IList<VendorModel> GetbindDocument12(int CompanyKey)
        {
            return __vendorservice.GetbindDocument12(CompanyKey);
        }
        public IList<VendorModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey)
        {
            return __vendorservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
        }
    }

}
