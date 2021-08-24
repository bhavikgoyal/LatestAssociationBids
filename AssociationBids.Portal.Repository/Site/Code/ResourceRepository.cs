using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class ResourceRepository : AssociationBids.Portal.Repository.Base.ResourceRepository, IResourceRepository
    {
        public ResourceRepository() 
            : base() { }

        public ResourceRepository(string connectionString)
            : base(connectionString) { }
    }
}
