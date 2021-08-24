using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class BidRepository : AssociationBids.Portal.Repository.Base.BidRepository, IBidRepository
    {
        public BidRepository() 
            : base() { }

        public BidRepository(string connectionString)
            : base(connectionString) { }
    }
}
