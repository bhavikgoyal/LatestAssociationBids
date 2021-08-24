using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class CompanyVendorRepository : AssociationBids.Portal.Repository.Base.CompanyVendorRepository, ICompanyVendorRepository
    {
        public CompanyVendorRepository() 
            : base() { }

        public CompanyVendorRepository(string connectionString)
            : base(connectionString) { }
    }
}
