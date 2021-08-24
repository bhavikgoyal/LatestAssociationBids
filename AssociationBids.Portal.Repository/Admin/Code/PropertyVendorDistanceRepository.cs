using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class PropertyVendorDistanceRepository : AssociationBids.Portal.Repository.Site.PropertyVendorDistanceRepository, IPropertyVendorDistanceRepository
    {
        public PropertyVendorDistanceRepository() 
            : base() { }

        public PropertyVendorDistanceRepository(string connectionString)
            : base(connectionString) { }
    }
}
