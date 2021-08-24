using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class LookUpRepository : AssociationBids.Portal.Repository.Base.LookUpRepository, ILookUpRepository
    {
        public LookUpRepository() 
            : base() { }

        public LookUpRepository(string connectionString)
            : base(connectionString) { }
    }
}
