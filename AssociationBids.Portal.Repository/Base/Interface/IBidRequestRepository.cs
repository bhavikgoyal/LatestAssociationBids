using System;
using System.Collections.Generic;
using System.Data;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IBidRequestRepository : IBaseRepository
    {
        bool Create(BidRequestModel item);
        bool Update(BidRequestModel item);
        bool Delete(int id);

        int GetMaxBidRequestKey( string title);
        int DeleteDoc(string title, string FileName);
        BidRequestModel Get(int id);
        IList<BidRequestModel> GetAll();
        IList<BidRequestModel> GetAll(BidRequestFilterModel filter);
        IList<BidRequestModel> GetAll(BidRequestFilterModel filter, PagingModel paging);
        List<BidRequestModel> SearchBidRequest(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey);
        List<BidRequestModel> SearchBidRequestPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey);
        List<BidRequestModel> SearchBidRequestVender(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int64 BidRequestKey);
        string SearchBidRequestVenderjson(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int64 BidVendorKey, Int64 CompanyKey, Int64 UserId, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController, long ResourceKey = 0);
        string SearchBidRequestVenderjsonPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int64 BidVendorKey, Int64 CompanyKey, Int64 UserId, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController, long ResourceKey);
        string SearchBidRequestVenderDocjson(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int64 BidVendorKey, string TableName);
        string ApceptRejectVenderBidrequest(Int64 BidVendorKey, string Status, string IsAssigned);
        string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title);
        string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus);
        string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId);
        Int64 SubmitVenderBid(Int64 BidVendorKey, string Status, Int64 ResourceKey, string Title, string Total, string Description, string LastModificationTime);
        string GetVenderBidList(Int64 BidVendorKey, Int64 ResourceKey);
        Int64 DocumentInsertDynamic(string ObjectKey, string FileName, string FileSize, string Controller, string Action);
        string GetDocumentListDynmc(Int64 ObjectKey, string ControllerName, string ActionName);
        string DeleteDocumentListDynmc(Int64 DocumentKey);
        List<System.Web.Mvc.SelectListItem> GetStatusListForDDL(string para1, string para2);

        IList<BidRequestModel> GetAllProperty(Int32 ResourceKey, Int32 companyKey);
        IList<LookUpModel> GetBidRequetStatusFilter();
        IList<BidRequestModel> GetAllService();
        bool DocInsert(BidRequestModel bidRequestModel, string FileName, string FileSize);
        bool UpdateStatus(string status, int BidRequestKey);
        DataTable UpdateStatusDBReturb(string status, int BidRequestKey);
        int UpdateStatusDBReturb1(string status, int BidRequestKey, int ParentBidRequest);
        int UpdateStatusDBReturbCancel(string status, int BidRequestKey);
        List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey);
        List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, long ResourceKey);
        List<BidRequestModel> SearchAllVendor(int BidRequestKey, string SearchVendorName, string SearchCompanyName, int IsStaredVendor, int LastWorkedBefore);

        bool VendorAddForService(int VendorKey, int serviceKey, int BidRequestKey, DateTime ResponseDueDate, int ResourceKey, string BidVendorID, string btval);

        BidRequestModel GetDataViewEdit(int id);
        BidRequestModel bindvendorinformation(int BidRequestKey);
        IList<BidRequestModel> GetbindDocument(int VendorKey, int ModuleKey);
        IList<BidRequestModel> GetbindWorkOrderDocument(int VendorKey);
        IList<BidRequestModel> Getbindservice(int VendorKey);
        BidRequestModel getbiddate(Int32 Comapanykey);
        IList<BidRequestModel> Searchinsurance(int VendorKey);
        BidRequestModel GetDataBidRequestViewEdit(int id);
        bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey);
        bool DocumentDelete(int BidRequestKey, string Docname);
        IList<BidRequestModel> GetbindBidRequestDocument(int BidRequestKey);
        bool InsertNotes(string title, string description, int BidRequestKey, int Resourcekey);
        IList<BidRequestModel> GetbindBidRequestNotes(int BidRequestKey);
        bool NotesRemove(int Noteid);
        bool InsertRating(string Message, int Rating1, int Rating2, int Rating3, int ResourceKey, int BidrequestKey);
        bool InsertRatingNew(string Message, int Rating1, int ResourceKey, int BidrequestKey);

        BidVendorModel GetBidVendorByBidVendorKey(int BidVendorKey);
        bool DeleteBidVendorByBidVendorKey(int BidVendorKey);
        bool UpdateResponseDueDate(string ResponseDueDate, int BidRequestKey, int BidVendorKey);
        bool BidRequestStatusUpdate(int BidRequestKey, string status);

        List<BidRequestModel> LoadBidStatus(long ResourceKey);

        int PMWorkOrdersCheckComeFromBIdRequest(string BidRequestKey);

        bool DateExtensionRequest(string BidName, string ManagerName, string ManagerCompanyName, string ManagerEmail, string VendorName);

        VendorModel BindVendorDetail(int BidRequestKey, int ResourceKey, string EmailSend);
    }
}

