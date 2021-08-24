using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class ServiceAreaRepository : AssociationBids.Portal.Repository.Base.ServiceAreaRepository, IServiceAreaRepository
    {
        public ServiceAreaRepository() 
            : base() { }

        public ServiceAreaRepository(string connectionString)
            : base(connectionString) { }
    }
}
