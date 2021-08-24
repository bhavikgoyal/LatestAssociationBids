using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class ServiceAreaRepository : AssociationBids.Portal.Repository.Site.ServiceAreaRepository, IServiceAreaRepository
    {
        public ServiceAreaRepository() 
            : base() { }

        public ServiceAreaRepository(string connectionString)
            : base(connectionString) { }
    }
}
