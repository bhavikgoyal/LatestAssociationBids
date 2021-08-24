using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class BidRequestRepository : AssociationBids.Portal.Repository.Base.BidRequestRepository, IBidRequestRepository
    {
        public BidRequestRepository() 
            : base() { }

        public BidRequestRepository(string connectionString)
            : base(connectionString) { }
    }
}
