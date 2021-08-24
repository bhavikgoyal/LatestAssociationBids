using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class CompanyRepository : AssociationBids.Portal.Repository.Base.CompanyRepository, ICompanyRepository
    {
        public CompanyRepository() 
            : base() { }

        public CompanyRepository(string connectionString)
            : base(connectionString) { }
    }
}
