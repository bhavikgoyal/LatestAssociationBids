using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class PricingRepository : AssociationBids.Portal.Repository.Base.PricingRepository, IPricingRepository
    {
        public PricingRepository() 
            : base() { }

        public PricingRepository(string connectionString)
            : base(connectionString) { }
    }
}
