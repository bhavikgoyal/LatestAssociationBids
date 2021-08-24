using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class InsuranceRepository : AssociationBids.Portal.Repository.Site.InsuranceRepository, IInsuranceRepository
    {
        public InsuranceRepository() 
            : base() { }

        public InsuranceRepository(string connectionString)
            : base(connectionString) { }
    }
}
