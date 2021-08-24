using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class LookUpRepository : AssociationBids.Portal.Repository.Site.LookUpRepository, ILookUpRepository
    {
        public LookUpRepository() 
            : base() { }

        public LookUpRepository(string connectionString)
            : base(connectionString) { }
    }
}
