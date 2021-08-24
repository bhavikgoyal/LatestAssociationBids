using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class PromotionRepository : AssociationBids.Portal.Repository.Site.PromotionRepository, IPromotionRepository
    {
        public PromotionRepository() 
            : base() { }

        public PromotionRepository(string connectionString)
            : base(connectionString) { }
    }
}
