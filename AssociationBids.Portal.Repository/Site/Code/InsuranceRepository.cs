using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class InsuranceRepository : AssociationBids.Portal.Repository.Base.InsuranceRepository, IInsuranceRepository
    {
        public InsuranceRepository() 
            : base() { }

        public InsuranceRepository(string connectionString)
            : base(connectionString) { }
    }
}
