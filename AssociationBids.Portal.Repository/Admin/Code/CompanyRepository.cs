using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class CompanyRepository : AssociationBids.Portal.Repository.Site.CompanyRepository, ICompanyRepository
    {
        public CompanyRepository() 
            : base() { }

        public CompanyRepository(string connectionString)
            : base(connectionString) { }
    }
}
