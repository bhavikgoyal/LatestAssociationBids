using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class PropertyResourceRepository : AssociationBids.Portal.Repository.Base.PropertyResourceRepository, IPropertyResourceRepository
    {
        public PropertyResourceRepository() 
            : base() { }

        public PropertyResourceRepository(string connectionString)
            : base(connectionString) { }
    }
}
