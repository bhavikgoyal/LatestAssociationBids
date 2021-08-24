using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class VendorServiceRepository : AssociationBids.Portal.Repository.Site.VendorServiceRepository, IVendorServiceRepository
    {
        public VendorServiceRepository() 
            : base() { }

        public VendorServiceRepository(string connectionString)
            : base(connectionString) { }
    }
}
