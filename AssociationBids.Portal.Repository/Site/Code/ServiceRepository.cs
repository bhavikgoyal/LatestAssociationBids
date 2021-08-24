using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class ServiceRepository : AssociationBids.Portal.Repository.Base.ServiceRepository, IServiceRepository
    {
        public ServiceRepository() 
            : base() { }

        public ServiceRepository(string connectionString)
            : base(connectionString) { }
    }
}
