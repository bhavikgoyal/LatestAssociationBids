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
    public class VendorManagerService : BaseService, IVendorManagerService
    {
        protected IVendorManagerRepository __vendorservice;

        public VendorManagerService()
         : this(new VendorManagerRepository()) { }

        public VendorManagerService(string connectionString)
           : this(new VendorManagerRepository(connectionString)) { }

        public VendorManagerService(VendorManagerRepository vendorRepository)
        {
            ConnectionString = vendorRepository.ConnectionString;

            __vendorservice = vendorRepository;


        }

        public List<VendorManagerVendorModel> SearchVendor(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            return __vendorservice.SearchVendor(PageSize, PageIndex, Search, Sort);
        }
        public List<VendorManagerVendorModel> SearchApprovedVendor(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __vendorservice.SearchApprovedVendor(PageSize, PageIndex, Search, Sort);
        }
        public List<VendorManagerVendorModel> SearchUnapprovedVendor(long PageSize, long PageIndex, string Search, string Sort,long ResourceKey, string Duplicate)
        {
            return __vendorservice.SearchUnapprovedVendor(PageSize, PageIndex, Search, Sort,ResourceKey, Duplicate);
        }
        public List<VendorManagerVendorModel> SearchUnapprovedVendorPriority(long PageSize, long PageIndex, string Search, string Sort, long ResourceKey, string Duplicate)
        {
            return __vendorservice.SearchUnapprovedVendorPriority(PageSize, PageIndex, Search, Sort, ResourceKey,Duplicate);
        }
        public List<VendorManagerVendorModel> SearchPendingVendor(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __vendorservice.SearchPendingVendor(PageSize, PageIndex, Search, Sort);
        }

        public IList<BidRequestModel> BidRequestsIndexPagingByCompanyKey(int CompanyKey, int index, int pageSize, string Search, string sort)
        {
            return __vendorservice.BidRequestsIndexPagingByCompanyKey(CompanyKey, index, pageSize, Search, sort);
        }
        public IList<BidRequestModel> WordOrderIndexPagingByCompanyKey(int CompanyKey, int index, int pageSize, string Search, string sort)
        {
            return __vendorservice.WordOrderIndexPagingByCompanyKey(CompanyKey, index, pageSize, Search, sort);
        }

        public IList<VendorManagerVendorModel> GetAllState()
        {
            return __vendorservice.GetAllState();
        }

        public IList<VendorManagerVendorModel> GetAllService()
        {
            return __vendorservice.GetAllService();
        }


        public VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey)
        {
            return __vendorservice.GetVendorByCompanyKey(CompanyKey);
        }

        public VendorManagerVendorModel GetVendorByCompanyKeyForInviteView(int CompanyKey)
        {
            return __vendorservice.GetVendorByCompanyKeyForInviteView(CompanyKey);
        }

        public ResourceModel GetResourceForInviteVendor(int CompanyKey)
        {
            return __vendorservice.GetResourceForInviteVendor(CompanyKey);
        }

        public VendorManagerVendorModel GetUnapprovedVendorByCompanyKey(int CompanyKey)
        {
            return __vendorservice.GetUnapprovedVendorByCompanyKey(CompanyKey);
        }

        public ResourceModel GetResourceByCompanyKey(int CompanyKey)
        {
            return __vendorservice.GetResourceByCompanyKey(CompanyKey);
        }
        public IList<VendorManagerVendorModel> GetServiceByCompany(int CompanyKey)
        {
            return __vendorservice.GetServiceByCompany(CompanyKey);
        }
        public long VendorManagerServiceDelete(int CompanyKey,int VendorServiceKey)
        {
            return __vendorservice.VendorManagerServiceDelete(CompanyKey, VendorServiceKey);
        }
        public long VendorManagerUpdate(VendorManagerVendorModel item)
        {
            return __vendorservice.VendorManagerEdit(item);
        }
        public IList<VendorManagerModel> Getbindservice(int CompanyKey)
        {
            return __vendorservice.Getbindservice(CompanyKey);
        }
        public long VendorManagerAddInsurance(InsuranceModel item)
        {
            return __vendorservice.VendorManagerAddInsurance(item);
        }
        public IList<VendorManagerModel> GetbindDocument(int CompanyKey)
        {
            return __vendorservice.GetbindDocument(CompanyKey);
        }
        public IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey)
        {
            return __vendorservice.GetbindDocumentByInsuranceKey(CompanyKey,InsuranceKey);
        }
        public bool VendorManagerRemoveDocument(int DocumentKey)
        {
            return __vendorservice.VendorManagerRemoveDocument(DocumentKey);
        }
        public bool VendorManagerRemoveInsurance(int InsuranceKey)
        {
            return __vendorservice.VendorManagerRemoveInsurance(InsuranceKey);
        }
        public VendorManagerModel GetUserPrimaryByCompanyKeyAndResourceKey(int CompanyKey,int ResourceKey)
        {
            return __vendorservice.GetUserPrimaryByCompanyKeyAndResourceKey(CompanyKey, ResourceKey);
        }
        public Int64 VendorManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey)
        {
            return __vendorservice.VendorManagerInviteVendor(item, ResourceKey);
        }
        public Int64 RegVendorManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey)
        {
            return __vendorservice.RegVendorManagerInviteVendor(item, ResourceKey);
        }
        public Int64 RegistrationManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey)
        {
            return __vendorservice.RegistrationManagerInviteVendor(item, ResourceKey);
        }
        public List<VendorManagerVendorModel> VendorManager_GetAllManagementCompany()
        {
            return __vendorservice.VendorManager_GetAllManagementCompany();
        }

        public Int64 VendorManager_Update_InviteVendor(VendorManagerVendorModel item)
        {
            return __vendorservice.VendorManager_Update_InviteVendor(item);
        }
        public bool VendorManagerApproveVendor(int CompanyKey)
        {
            return __vendorservice.VendorManagerApproveVendor(CompanyKey);
        }

        public bool VendorManagerMarkDuplicateVendor(int CompanyKey)
        {
            return __vendorservice.VendorManagerMarkDuplicateVendor(CompanyKey);
        }

        public void mailsendAsync(int status, string fromemail)
        {
            __vendorservice.mailsendAsync(status, fromemail);
        }
        public List<bool> CheckDuplicatedEmailAndCompanyName(string Email, string CompanyName)
        {
            return __vendorservice.CheckDuplicatedEmailAndCompanyName(Email,CompanyName);
        }
        public bool CheckDuplicatedEmailByResourceKey(string Email, int ResourceKey)
        {
            return __vendorservice.CheckDuplicatedEmailByResourceKey(Email, ResourceKey);
        }

        public bool CheckDuplicatedCompanyNameByCompanyKey(string CompanyName, int CompanyKey)
        {
            return __vendorservice.CheckDuplicatedCompanyNameByCompanyKey(CompanyName, CompanyKey);
        }

        public bool VendorManagerChangePassword(string NewPassword, int UserKey)
        {
            return __vendorservice.VendorManagerChangePassword(NewPassword, UserKey);
        }
        public VendorManagerModel VendorManagerGetBidRequestDetails(int BidRequestKey, int CompanyKey)
        {
            return __vendorservice.VendorManagerGetBidRequestDetails(BidRequestKey, CompanyKey);
        }

        public string SearchBidRequestVenderDocjson(long PageSize, long PageIndex, string Search, string Sort, long BidVendorKey, string TableName)
        {
            return __vendorservice.SearchBidRequestVenderDocjson(PageSize, PageIndex, Search, Sort, BidVendorKey, TableName);
        }
        public string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName)
        {
            return __vendorservice.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName);
        }
        public string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            return __vendorservice.SendInsertMessageList(ObjectKey, ResourceKey, ModuleKeyName, UpdatemsgStatus);
        }
        public string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId)
        {
            return __vendorservice.MessageNewCount(ObjectKey, ResourceKey, ModuleKeyName, UserId);
        }


        public VendorManagerModel VendorManagerGetWorkOrderDetails(int BidRequestKey, int CompanyKey)
        {
            return __vendorservice.VendorManagerGetWorkOrderDetails(BidRequestKey, CompanyKey);
        }
        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            return __vendorservice.SearchVendorByBidRequest(BidRequestKey, modulekey);
        }
        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey,int Resourcekey)
        {
            return __vendorservice.SearchVendorByBidRequest(BidRequestKey, modulekey, Resourcekey);
        }

        public List<BidRequestModel> SearchAllVendor(int BidRequestKey, string SearchVendorName, string SearchCompanyName, int IsStaredVendor, int LastWorkedBefore)
        {
            return __vendorservice.SearchAllVendor(BidRequestKey, SearchVendorName, SearchCompanyName, IsStaredVendor, LastWorkedBefore);
        }

        public IList<BidRequestModel> GetbindDocumentByBidRequestKey(int BidRequestKey, int ModuleKey)
        {
            return __vendorservice.GetbindDocumentByBidRequestKey(BidRequestKey, ModuleKey);
        }

        public IList<BidRequestModel> GetbindBidRequestNotes(int BidRequestKey)
        {
            return __vendorservice.GetbindBidRequestNotes(BidRequestKey);
        }

        public bool InsertNotes(string title, string description, int BidRequestKey, int Resourcekey)
        {
            return __vendorservice.InsertNotes(title, description, BidRequestKey, Resourcekey);
        }

        public bool NotesRemove(int Noteid)
        {
            return __vendorservice.NotesRemove(Noteid);
        }
       public bool EditService (int ComoanyKey,string servicekey)
        {
            return __vendorservice.EditService(ComoanyKey,servicekey);
        }

        public IList<VendorManagerVendorModel> AppoGetAllService(string PleaseSelect)
        {
            return __vendorservice.AppoGetAllService(PleaseSelect);
        }

        public List<InsuranceModel> GetInsuranceByCompanyKey(int CompanyKey)
        {
            return __vendorservice.GetInsuranceByCompanyKey(CompanyKey);
        }
    }
}
