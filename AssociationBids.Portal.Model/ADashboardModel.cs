using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class ADashboardModel
    {
        public string OpenBidRequest { get; set; }
        public string Type { get; set; }
        public string BidValue { get; set; }
        public string AwardedBidRequest { get; set; }
        public string ClosedBidRequest { get; set; }
        public string TotalAmount { get; set; }
        public string ActiveVendors { get; set; }
        public string RenewedVendors { get; set; }
        public string CancelMembership { get; set; }
        public string lastyearActiveVendors { get; set; }
        public string LastYearRenewedVendors { get; set; }
        public string LastYearCancelMembership { get; set; }
        public string Monthly { get; set; }
        public string Quaterly { get; set; }
        public string Yearly { get; set; }
  
      
        public string NotIntersted { get; set; }
        public string BidNotSubmitted { get; set; }
        public string BidSubmitted { get; set; }
        public string Awarded { get; set; }
        public string Rejected { get; set; }
      
        public string dayOfYear { get; set; }
        public string projectcount { get; set; }



    }

    public class ADashboardModelC
    {
     public string Type { get; set; }
     public string BidValue { get; set; }
     public string TotalAmount { get; set; }
}
    public class ADashboardModelLineChart
    {

        public string projectcount { get; set; }
        public string dayOfYear { get; set; }
    }

}
