using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class PricingRepository : AssociationBids.Portal.Repository.Site.PricingRepository, IPricingRepository
    {
        public PricingRepository() 
            : base() { }

        public PricingRepository(string connectionString)
            : base(connectionString) { }
    }
}
