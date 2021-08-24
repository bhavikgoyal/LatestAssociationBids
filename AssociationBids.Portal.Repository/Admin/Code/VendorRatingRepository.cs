using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class VendorRatingRepository : AssociationBids.Portal.Repository.Site.VendorRatingRepository, IVendorRatingRepository
    {
        public VendorRatingRepository() 
            : base() { }

        public VendorRatingRepository(string connectionString)
            : base(connectionString) { }
    }
}
