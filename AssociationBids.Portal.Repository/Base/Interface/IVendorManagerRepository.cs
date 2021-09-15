using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IVendorManagerRepository : IBaseRepository
    {
        List<VendorManagerVendorModel> SearchVendor(Int64 PageSize, Int64 PageIndex, string Search, String Sort);
        List<VendorManagerVendorModel> SearchApprovedVendor(long PageSize, long PageIndex, string Search, string Sort);
        List<VendorManagerVendorModel> SearchPendingVendor(long PageSize, long PageIndex, string Search, string Sort);
        List<VendorManagerVendorModel> SearchUnapprovedVendor(long PageSize, long PageIndex, string Search, string Sort,long ResourceKey,string Duplicate);
        List<VendorManagerVendorModel> SearchUnapprovedVendorPriority(long PageSize, long PageIndex, string Search, string Sort, long ResourceKey, string Duplicate);
        IList<BidRequestModel> BidRequestsIndexPagingByCompanyKey(int CompanyKey, int index, int pageSize, string Search, string sort);
        IList<BidRequestModel> WordOrderIndexPagingByCompanyKey(int CompanyKey, int index, int pageSize, string Search, string sort);

        IList<VendorManagerVendorModel> GetServiceByCompany(int CompanyKey);
        IList<VendorManagerVendorModel> GetAllState();

        IList<VendorManagerVendorModel> GetAllService();
        IList<VendorManagerVendorModel> AppoGetAllService(string PleaseSelect);

        Int64 VendorManagerEdit(Model.VendorManagerVendorModel item);

        Int64 VendorManagerServiceDelete(int CompanyKey, int VendorServiceKey);

        VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey);

        VendorManagerVendorModel GetVendorByCompanyKeyForInviteView(int CompanyKey);

        VendorManagerVendorModel GetUnapprovedVendorByCompanyKey(int CompanyKey);

        ResourceModel GetResourceByCompanyKey(int CompanyKey);

        ResourceModel GetResourceForInviteVendor(int CompanyKey);

        IList<VendorManagerModel> Getbindservice(int CompanyKey);

        Int64 VendorManagerAddInsurance(InsuranceModel item);

        IList<VendorManagerModel> GetbindDocument(int CompanyKey);

        IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey);

        bool VendorManagerRemoveDocument(int DocumentKey);

        bool VendorManagerRemoveInsurance(int InsuranceKey);

        VendorManagerModel GetUserPrimaryByCompanyKeyAndResourceKey(int CompanyKey, int ResourceKey);

        Int64 VendorManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey);
        Int64 RegistrationManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey);
        Int64 RegVendorManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey);

        List<VendorManagerVendorModel> VendorManager_GetAllManagementCompany();

        Int64 VendorManager_Update_InviteVendor(VendorManagerVendorModel item);

        List<bool> CheckDuplicatedEmailAndCompanyName(string Email,string CompanyName);

        bool CheckDuplicatedEmailByResourceKey(string Email, int ResourceKey);

        bool CheckDuplicatedCompanyNameByCompanyKey(string CompanyName, int CompanyKey);

        bool VendorManagerChangePassword(string NewPassword, int UserKey);

        bool VendorManagerApproveVendor(int CompanyKey);

        bool VendorManagerMarkDuplicateVendor(int CompanyKey);

        void mailsendAsync(int status, string fromemail);

        VendorManagerModel VendorManagerGetBidRequestDetails(int BidRequestKey, int CompanyKey);

        string SearchBidRequestVenderDocjson(long PageSize, long PageIndex, string Search, string Sort, long BidVendorKey, string TableName);
        string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName);
        string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus);
        string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId);

        VendorManagerModel VendorManagerGetWorkOrderDetails(int BidRequestKey, int CompanyKey);
        List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey);
        List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey,int resourcekey);

        List<BidRequestModel> SearchAllVendor(int BidRequestKey, string SearchVendorName, string SearchCompanyName, int IsStaredVendor, int LastWorkedBefore);

        IList<BidRequestModel> GetbindDocumentByBidRequestKey(int BidRequestKey, int ModuleKey);

        IList<BidRequestModel> GetbindBidRequestNotes(int BidRequestKey);
        bool InsertNotes(string title, string description, int BidRequestKey, int Resourcekey);

        bool NotesRemove(int Noteid);

        bool EditService(int ComoanyKey,string servicekey);

        List<InsuranceModel> GetInsuranceByCompanyKey(int CompanyKey);
    }
}
