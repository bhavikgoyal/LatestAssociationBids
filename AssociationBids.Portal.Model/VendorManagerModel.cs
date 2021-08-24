using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class VendorManagerModel
    {
        public VendorManagerVendorModel Vendor { get; set; }
        public ResourceModel Resource { get; set; }
        public VendorServiceModel ServiceModel { get; set; }
        public DocumentModel Document { get; set; }
        public InsuranceModel Insurance { get; set; }
        public UserModel UserModel { get; set; }
        public BidRequestModel BidRequest { get; set; }

        public string Favorite { get; set; }
        public int CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string CellPhone { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Email { get; set; }


        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        //ServiceArea
        public int Radius { get; set; }
        public string RadiusKey { get; set; }
        public string ServiceAddress { get; set; }

    }
}
