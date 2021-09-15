using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class SupportService : BaseService, ISupportService
    {
        protected ISupportRepository __repository;
        public SupportService()
            : this(new SupportRepository()) { }

        public SupportService(string connectionString)
            : this(new SupportRepository(connectionString)) { }

        public SupportService(ISupportRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }
        
        public List<SupportModel> SearchBidRequest(long PageSize, long PageIndex, string PropertyName, string VendorName, string CompanyName, string Sort, int BidStatus, Int32 Resourcekey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate)
        {
            return __repository.SearchBidRequest(PageSize, PageIndex, PropertyName, VendorName, CompanyName, Sort, BidStatus, Resourcekey, BidRequestStatus, Modulekey, FromDate, ToDate);
        }
        public List<SupportModel> SearchBidRequest1(long PageSize, long PageIndex, string PropertyName, string VendorName, string CompanyName, string Sort, int BidStatus, Int32 Resourcekey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate)
        {
            return __repository.SearchBidRequest1(PageSize, PageIndex, PropertyName, VendorName, CompanyName, Sort, BidStatus, Resourcekey, BidRequestStatus, Modulekey, FromDate, ToDate);
        }
    }
}