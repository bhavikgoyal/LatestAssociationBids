using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
namespace AssociationBids.Portal.Service.Base
{
    public interface IBidReportService : IBaseService
    {
        List<BidRequestModel> SearchBidRequest(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate);
        List<BidRequestModel> SearchBidRequestPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey);
        List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, long modulekey);
        bool Insert(ReportEmailModel item);

        string getVendorDate(int BidVendorKey);
        List<BidRequestModel> SearchVendorByWorkReport(int BidRequestKey, long modulekey,long ResourceKey,long CompanyKey, string BidRequestStatus, int PropertyKey, DateTime FromDate, DateTime ToDate);
        List<BidRequestModel> SearchVendorByPMWorkOrder(int BidRequestKey, int modulekey, long ResourceKey, string BidRequestStatus, int PropertyKey, DateTime FromDate, DateTime ToDate);
       
        List<BidRequestModel> EmailReport(string BidRequestKey, long modulekey, string VendorSelected);
        List<NoteModel> NotesByWorkReport(int BidRequestKey, int ModuleKey);




    }
}
