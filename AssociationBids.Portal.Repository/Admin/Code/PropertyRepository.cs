using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class PropertyRepository : AssociationBids.Portal.Repository.Site.PropertyRepository, IPropertyRepository
    {
        public PropertyRepository() 
            : base() { }

        public PropertyRepository(string connectionString)
            : base(connectionString) { }
    }
}
