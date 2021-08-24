using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class CompanyVendorRepository : AssociationBids.Portal.Repository.Site.CompanyVendorRepository, ICompanyVendorRepository
    {
        public CompanyVendorRepository() 
            : base() { }

        public CompanyVendorRepository(string connectionString)
            : base(connectionString) { }
    }
}
