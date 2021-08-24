using System;
using System.Collections.Generic;
using System.Data;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class BidRequestService : BaseService, IBidRequestService
    {
        protected IBidRequestRepository __repository;

        public BidRequestService()
            : this(new BidRequestRepository()) { }

        public BidRequestService(string connectionString)
            : this(new BidRequestRepository(connectionString)) { }

        public BidRequestService(IBidRequestRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(BidRequestModel item)
        {
            if (!Util.IsValidInt(item.PropertyKey))
            {
                AddError("PropertyKey", "Property can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidInt(item.ServiceKey))
            {
                AddError("ServiceKey", "Service can not be empty.");
            }
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
            }
            if (!Util.IsValidInt(item.BidRequestStatus))
            {
                AddError("BidRequestStatus", "Bid Request Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(BidRequestFilterModel filter)
        {
            BidRequestFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.PropertyKey != filter.PropertyKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.ServiceKey != filter.ServiceKey)
                return true;

            if (defaultFilter.BidRequestStatus != filter.BidRequestStatus)
                return true;

            return false;
        }

        public virtual BidRequestFilterModel CreateFilter()
        {
            BidRequestFilterModel filter = new BidRequestFilterModel();

            return UpdateFilter(filter);
        }

        public virtual BidRequestFilterModel CreateFilter(BidRequestModel item)
        {
            BidRequestFilterModel filter = new BidRequestFilterModel();

            //filter.PropertyKey = item.PropertyKey;
            filter.ResourceKey = item.ResourceKey;
            filter.ServiceKey = item.ServiceKey;
            filter.BidRequestStatus = item.BidRequestStatus;

            return UpdateFilter(filter);
        }

        public virtual BidRequestFilterModel UpdateFilter(BidRequestFilterModel filter)
        {
            return filter;
        }

        public virtual BidRequestModel Create()
        {
            ResetSiteSettings();

            BidRequestModel item = new BidRequestModel();

            return item;
        }

        public virtual bool Create(BidRequestModel item)
        {
            item.DateAdded = DateTime.Now;
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(BidRequestModel item)
        {
            item.LastModificationTime = DateTime.Now;
            return __repository.Update(item);

        }

        public virtual bool Delete(int id)
        {
            return __repository.Delete(id);
        }

        public virtual BidRequestModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<BidRequestModel> GetAll()
        {
            BidRequestFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<BidRequestModel> GetAll(BidRequestFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<BidRequestModel> GetAll(BidRequestFilterModel filter, PagingModel paging)
        {
            IList<BidRequestModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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

        public List<BidRequestModel> SearchBidRequest(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            return __repository.SearchBidRequest(PageSize, PageIndex, Search, Sort, Resourcekey, PropertyKey, BidRequestStatus, Modulekey);
        }

        public List<BidRequestModel> SearchBidRequestPriority(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            return __repository.SearchBidRequestPriority(PageSize, PageIndex, Search, Sort, Resourcekey, PropertyKey, BidRequestStatus, Modulekey);
        }
        public List<BidRequestModel> SearchBidRequestVender(long PageSize, long PageIndex, string Search, string Sort, Int64 BidRequestKey)
        {
            return __repository.SearchBidRequestVender(PageSize, PageIndex, Search, Sort, BidRequestKey);
        }
        public string SearchBidRequestVenderjson(long PageSize, long PageIndex, string Search, string Sort, Int64 BidVendorKey, Int64 CompanyKey, Int64 UserId, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController, long ResourceKey = 0)
        {
            return __repository.SearchBidRequestVenderjson(PageSize, PageIndex, Search, Sort, BidVendorKey, CompanyKey, UserId, BidRequestStatus, BiddueDateFrom, BiddueDateTo, ModuleController, ResourceKey);
        }
        public string SearchBidRequestVenderjsonPriority(long PageSize, long PageIndex, string Search, string Sort, Int64 BidVendorKey, Int64 CompanyKey, Int64 UserId, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController, long ResourceKey)
        {
            return __repository.SearchBidRequestVenderjsonPriority(PageSize, PageIndex, Search, Sort, BidVendorKey, CompanyKey, UserId, BidRequestStatus, BiddueDateFrom, BiddueDateTo, ModuleController, ResourceKey);
        }
        public string ApceptRejectVenderBidrequest(Int64 BidVendorKey, string Status, string IsAssigned)
        {
            return __repository.ApceptRejectVenderBidrequest(BidVendorKey, Status, IsAssigned);
        }
        public string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title)
        {
            return __repository.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName, Title);
        }
        public string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            return __repository.SendInsertMessageList(ObjectKey, ResourceKey, ModuleKeyName, UpdatemsgStatus);
        }
        public string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId)
        {
            return __repository.MessageNewCount(ObjectKey, ResourceKey, ModuleKeyName, UserId);
        }

        public string GetDocumentListDynmc(Int64 ObjectKey, string ControllerName, string ActionName)
        {
            return __repository.GetDocumentListDynmc(ObjectKey, ControllerName, ActionName);
        }
        public int GetMaxBidRequestKey( string title)
        {
            return __repository.GetMaxBidRequestKey(title);
        }

        public int DeleteDoc(string title,string FileName)
        {
            return __repository.DeleteDoc(title, FileName);
        }

        public string DeleteDocumentListDynmc(Int64 DocumentKey)
        {
            return __repository.DeleteDocumentListDynmc(DocumentKey);
        }
        public List<System.Web.Mvc.SelectListItem> GetStatusListForDDL(string para1, string para2)
        {
            return __repository.GetStatusListForDDL(para1, para2);
        }
        public Int64 SubmitVenderBid(Int64 BidVendorKey, string Status, Int64 ResourceKey, string Title, string Total, string Description, string LastModificationTime)
        {
            return __repository.SubmitVenderBid(BidVendorKey, Status, ResourceKey, Title, Total, Description, LastModificationTime);
        }
        public string GetVenderBidList(Int64 BidVendorKey, Int64 ResourceKey)
        {
            return __repository.GetVenderBidList(BidVendorKey, ResourceKey);
        }
        public Int64 DocumentInsertDynamic(string ObjectKey, string FileName, string FileSize, string Controller, string Action)
        {
            return __repository.DocumentInsertDynamic(ObjectKey, FileName, FileSize, Controller, Action);
        }

        public string SearchBidRequestVenderDocjson(long PageSize, long PageIndex, string Search, string Sort, Int64 BidVendorKey, string TableName)
        {
            return __repository.SearchBidRequestVenderDocjson(PageSize, PageIndex, Search, Sort, BidVendorKey, TableName);
        }
        public IList<BidRequestModel> GetAllProperty(Int32 ResourceKey, Int32 companyKey)
        {
            return __repository.GetAllProperty(ResourceKey, companyKey);
        }
        public virtual BidRequestModel getbiddate(int Comapanykey)
        {
            return __repository.getbiddate(Comapanykey);
        }
        public IList<LookUpModel> GetBidRequetStatusFilter()
        {
            return __repository.GetBidRequetStatusFilter();
        }

        public IList<BidRequestModel> GetAllService()
        {
            return __repository.GetAllService();
        }

        public virtual bool DocInsert(BidRequestModel bidRequestModel, string FileName, string FileSize)
        {
            return __repository.DocInsert(bidRequestModel, FileName, FileSize);
        }
        public virtual bool UpdateStatus(string status, int BidRequestKey)
        {
            return __repository.UpdateStatus(status, BidRequestKey);
        }
        public DataTable UpdateStatusDBReturb(string status, int BidRequestKey)
        {
            return __repository.UpdateStatusDBReturb(status, BidRequestKey);
        }
        public int UpdateStatusDBReturb1(string status, int BidRequestKey, int ParentbidRequest)
        {
            return __repository.UpdateStatusDBReturb1(status, BidRequestKey, ParentbidRequest);
        }
        public int UpdateStatusDBReturbCancel(string status, int BidRequestKey)
        {
            return __repository.UpdateStatusDBReturbCancel(status, BidRequestKey);
        }
        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            return __repository.SearchVendorByBidRequest(BidRequestKey, modulekey);
        }

        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, long ResourceKey)
        {
            return __repository.SearchVendorByBidRequest(BidRequestKey, modulekey, ResourceKey);
        }

        public List<BidRequestModel> SearchAllVendor(int BidRequestKey, string SearchVendorName, string SearchCompanyName, int IsStaredVendor, int LastWorkedBefore)
        {
            return __repository.SearchAllVendor(BidRequestKey, SearchVendorName, SearchCompanyName, IsStaredVendor, LastWorkedBefore);
        }

        public virtual bool VendorAddForService(int VendorKey, int ServiceKey, int BidRequestKey, DateTime ResponseDueDate, int ResourceKey, string BidVendorID, string btval)
        {
            return __repository.VendorAddForService(VendorKey, ServiceKey, BidRequestKey, ResponseDueDate, ResourceKey, BidVendorID, btval);
        }

        public virtual BidRequestModel GetDataViewEdit(int id)
        {
            return __repository.GetDataViewEdit(id);
        }





        public IList<BidRequestModel> GetbindDocument(int VendorKey, int ModuleKey)
        {
            return __repository.GetbindDocument(VendorKey, ModuleKey);
        }

        public IList<BidRequestModel> GetbindWorkOrderDocument(int VendorKey)
        {
            return __repository.GetbindWorkOrderDocument(VendorKey);
        }

        public IList<BidRequestModel> Getbindservice(int VendorKey)
        {
            return __repository.Getbindservice(VendorKey);
        }

        public IList<BidRequestModel> Searchinsurance(int VendorKey)
        {
            return __repository.Searchinsurance(VendorKey);
        }

        public virtual BidRequestModel GetDataBidRequestViewEdit(int id)
        {
            return __repository.GetDataBidRequestViewEdit(id);
        }

        public virtual bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey)
        {
            return __repository.UpdateDocInsert(BidRequestKey, FileName, FileSize, ModuleKey);
        }

        public virtual bool DocumentDelete(int BidRequestKey, string Docname)
        {
            return __repository.DocumentDelete(BidRequestKey, Docname);
        }
        public virtual bool InsertRating(string Message, int Rating1, int Rating2, int Rating3, int ResourceKey, int BidrequestKey)
        {
            return __repository.InsertRating(Message, Rating1, Rating2, Rating3, ResourceKey, BidrequestKey);
        }


        public virtual bool InsertRatingNew(string Message, int Rating1, int ResourceKey, int BidrequestKey)
        {
            return __repository.InsertRatingNew(Message, Rating1, ResourceKey, BidrequestKey);
        }

        public BidRequestModel bindvendorinformation(int BidRequestKey)
        {
            return __repository.bindvendorinformation(BidRequestKey);
        }

        public IList<BidRequestModel> GetbindBidRequestDocument(int BidRequestKey)
        {
            return __repository.GetbindBidRequestDocument(BidRequestKey);
        }

        public virtual bool InsertNotes(string title, string description, int BidRequestKey, int Resourcekey)
        {
            return __repository.InsertNotes(title, description, BidRequestKey, Resourcekey);
        }

        public IList<BidRequestModel> GetbindBidRequestNotes(int BidRequestKey)
        {
            return __repository.GetbindBidRequestNotes(BidRequestKey);
        }
        public virtual bool NotesRemove(int Noteid)
        {
            return __repository.NotesRemove(Noteid);
        }


        public BidVendorModel GetBidVendorByBidVendorKey(int BidVendorKey)
        {
            return __repository.GetBidVendorByBidVendorKey(BidVendorKey);
        }
        public bool DeleteBidVendorByBidVendorKey(int BidVendorKey)
        {
            return __repository.DeleteBidVendorByBidVendorKey(BidVendorKey);
        }
        public bool BidRequestStatusUpdate(int BidRequestKey, string status)
        {
            return __repository.BidRequestStatusUpdate(BidRequestKey, status);
        }
        public bool UpdateResponseDueDate(string ResponseDueDate, int BidRequestKey, int BidVendorKey)
        {
            return __repository.UpdateResponseDueDate(ResponseDueDate, BidRequestKey, BidVendorKey);
        }
        public List<BidRequestModel> LoadBidStatus(long ResourceKey)
        {
            return __repository.LoadBidStatus(ResourceKey);
        }
        public int PMWorkOrdersCheckComeFromBIdRequest(string BidRequestKey)
        {
            return __repository.PMWorkOrdersCheckComeFromBIdRequest(BidRequestKey);
        }
        public bool DateExtensionRequest(string BidName, string ManagerName, string ManagerCompanyName, string ManagerEmail, string VendorName)
        {
            return __repository.DateExtensionRequest(BidName, ManagerName, ManagerCompanyName, ManagerEmail, VendorName);
        }
        public VendorModel BindVendorDetail(int BidRequestKey, int ResourceKey, string EmailSend)
        {
            return __repository.BindVendorDetail(BidRequestKey, ResourceKey, EmailSend);
        }
    }
}
