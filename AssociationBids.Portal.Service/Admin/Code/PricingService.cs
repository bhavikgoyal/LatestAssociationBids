using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class PricingService : AssociationBids.Portal.Service.Site.PricingService, IPricingService
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

        public override PricingFilterModel UpdateFilter(PricingFilterModel filter)
        {
            return filter;
        }
    }
}
