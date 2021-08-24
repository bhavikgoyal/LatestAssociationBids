using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class PricingService : AssociationBids.Portal.Service.Base.PricingService, IPricingService
    {
        new protected IPricingRepository __repository;

        public PricingService()
            : this(new PricingRepository()) { }

        public PricingService(string connectionString)
            : this(new PricingRepository(connectionString)) { }

        public PricingService(IPricingRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
