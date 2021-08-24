using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class VendorRatingRepository : AssociationBids.Portal.Repository.Base.VendorRatingRepository, IVendorRatingRepository
    {
        public VendorRatingRepository() 
            : base() { }

        public VendorRatingRepository(string connectionString)
            : base(connectionString) { }
    }
}
