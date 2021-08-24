using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class ServiceRepository : AssociationBids.Portal.Repository.Site.ServiceRepository, IServiceRepository
    {
        public ServiceRepository() 
            : base() { }

        public ServiceRepository(string connectionString)
            : base(connectionString) { }
    }
}
