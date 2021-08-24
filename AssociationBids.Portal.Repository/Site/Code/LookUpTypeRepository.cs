using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class LookUpTypeRepository : AssociationBids.Portal.Repository.Base.LookUpTypeRepository, ILookUpTypeRepository
    {
        public LookUpTypeRepository() 
            : base() { }

        public LookUpTypeRepository(string connectionString)
            : base(connectionString) { }
    }
}
