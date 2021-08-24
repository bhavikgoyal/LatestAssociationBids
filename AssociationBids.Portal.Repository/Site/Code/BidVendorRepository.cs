using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class BidVendorRepository : AssociationBids.Portal.Repository.Base.BidVendorRepository, IBidVendorRepository
    {
        public BidVendorRepository() 
            : base() { }

        public BidVendorRepository(string connectionString)
            : base(connectionString) { }
    }
}
