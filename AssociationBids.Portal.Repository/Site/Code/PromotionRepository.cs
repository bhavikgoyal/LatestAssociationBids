using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class PromotionRepository : AssociationBids.Portal.Repository.Base.PromotionRepository, IPromotionRepository
    {
        public PromotionRepository() 
            : base() { }

        public PromotionRepository(string connectionString)
            : base(connectionString) { }
    }
}
