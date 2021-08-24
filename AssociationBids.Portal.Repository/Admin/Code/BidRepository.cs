using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class BidRepository : AssociationBids.Portal.Repository.Site.BidRepository, IBidRepository
    {
        public BidRepository() 
            : base() { }

        public BidRepository(string connectionString)
            : base(connectionString) { }
    }
}
