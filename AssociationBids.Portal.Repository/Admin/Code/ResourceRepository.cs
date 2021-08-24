using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class ResourceRepository : AssociationBids.Portal.Repository.Site.ResourceRepository, IResourceRepository
    {
        public ResourceRepository() 
            : base() { }

        public ResourceRepository(string connectionString)
            : base(connectionString) { }
    }
}
