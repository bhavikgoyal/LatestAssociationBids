using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class BidRequestRepository : AssociationBids.Portal.Repository.Site.BidRequestRepository, IBidRequestRepository
    {
        public BidRequestRepository() 
            : base() { }

        public BidRequestRepository(string connectionString)
            : base(connectionString) { }
    }
}
