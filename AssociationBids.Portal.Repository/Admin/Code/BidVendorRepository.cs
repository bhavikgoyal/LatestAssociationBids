using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class BidVendorRepository : AssociationBids.Portal.Repository.Site.BidVendorRepository, IBidVendorRepository
    {
        public BidVendorRepository() 
            : base() { }

        public BidVendorRepository(string connectionString)
            : base(connectionString) { }
    }
}
