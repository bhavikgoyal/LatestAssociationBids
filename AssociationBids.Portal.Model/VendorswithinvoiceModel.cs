using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace AssociationBids.Portal.Model
{
    public class VendorswithinvoiceModel : BaseModel
    {
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public Decimal Balance { get; set; }
        public string InvoiceDate { get; set; }
        public Int32 TotalRecords { get; set; }
        public int Length { get; set; }
        public string StateKey { get; set; }
        public string State { get; set; }
        public string ServiceName { get; set; }
        public Decimal TotalBidValue { get; set; }
        public string ContactPerson { get; set; }
        public string AcceptBid { get; set; }
        public string RejectedBid { get; set; }
        public string SubmittedBid { get; set; }

    }
}
