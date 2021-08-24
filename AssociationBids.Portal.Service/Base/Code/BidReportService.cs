using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
namespace AssociationBids.Portal.Service.Base
{
    public class BidReportService : BaseService, IBidReportService
    {
        protected IBidReportRepository __repository;

        public BidReportService()
            : this(new BidReportRepository()) { }

        public BidReportService(string connectionString)
            : this(new BidReportRepository(connectionString)) { }

        public BidReportService(IBidReportRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }
        
        public bool Insert(ReportEmailModel item)
        {
            return __repository.Insert(item);
        }
        public List<BidRequestModel> SearchBidRequest(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate)
        {
            return __repository.SearchBidRequest(PageSize, PageIndex, Search, Sort, Resourcekey, PropertyKey, BidRequestStatus, Modulekey,FromDate,ToDate);
        }

        public List<BidRequestModel> SearchBidRequestPriority(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            return __repository.SearchBidRequestPriority(PageSize, PageIndex, Search, Sort, Resourcekey, PropertyKey, BidRequestStatus, Modulekey);
        }
        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, long modulekey)
        {
            return __repository.SearchVendorByBidRequest(BidRequestKey, modulekey);
        }
        public List<BidRequestModel> SearchVendorByWorkReport(int BidRequestKey, long modulekey,long ResourceKey,long CompanyKey, string BidRequestStatus, int PropertyKey, DateTime FromDate, DateTime ToDate)
        {
            return __repository.SearchVendorByWorkReport(BidRequestKey, modulekey, ResourceKey, CompanyKey, BidRequestStatus, PropertyKey, FromDate, ToDate);
        }
        public List<BidRequestModel> EmailReport(string BidRequestKey, long modulekey,string VendorSelected)
        {
            return __repository.EmailReport(BidRequestKey, modulekey, VendorSelected);
        }
      
        public List<BidRequestModel> SearchVendorByPMWorkOrder(int BidRequestKey, int modulekey, long ResourceKey, string BidRequestStatus, int PropertyKey, DateTime FromDate, DateTime ToDate)
        {
            return __repository.SearchVendorByPMWorkOrder(BidRequestKey, modulekey, ResourceKey, BidRequestStatus, PropertyKey, FromDate, ToDate);
        }

        public string getVendorDate(int BidVendorKey)
        {
            return __repository.getVendorDate(BidVendorKey);
        }



        public List<NoteModel> NotesByWorkReport(int BidRequestKey, int ModuleKey)
        {
            return __repository.NotesByWorkReport(BidRequestKey, ModuleKey);
        }
    }
}
