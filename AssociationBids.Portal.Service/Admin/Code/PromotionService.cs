using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class PromotionService : AssociationBids.Portal.Service.Site.PromotionService, IPromotionService
    {
        new protected IPromotionRepository __repository;

        public PromotionService()
            : this(new PromotionRepository()) { }

        public PromotionService(string connectionString)
            : this(new PromotionRepository(connectionString)) { }

        public PromotionService(IPromotionRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override PromotionFilterModel UpdateFilter(PromotionFilterModel filter)
        {
            return filter;
        }
    }
}
