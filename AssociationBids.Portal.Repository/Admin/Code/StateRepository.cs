using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class StateRepository : AssociationBids.Portal.Repository.Site.StateRepository, IStateRepository
    {
        public StateRepository() 
            : base() { }

        public StateRepository(string connectionString)
            : base(connectionString) { }
    }
}
