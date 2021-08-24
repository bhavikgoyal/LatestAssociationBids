using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class PortalRepository : AssociationBids.Portal.Repository.Site.PortalRepository, IPortalRepository
    {
        public PortalRepository() 
            : base() { }

        public PortalRepository(string connectionString)
            : base(connectionString) { }
    }
}
