using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;



namespace AssociationBids.Portal.Service.Site
{
    public class PromotionService : AssociationBids.Portal.Service.Base.PromotionService, IPromotionService
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
    }
}
