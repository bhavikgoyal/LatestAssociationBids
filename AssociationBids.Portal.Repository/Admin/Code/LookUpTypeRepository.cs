using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class LookUpTypeRepository : AssociationBids.Portal.Repository.Site.LookUpTypeRepository, ILookUpTypeRepository
    {
        public LookUpTypeRepository() 
            : base() { }

        public LookUpTypeRepository(string connectionString)
            : base(connectionString) { }
    }
}
