using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class PropertyVendorDistanceService : AssociationBids.Portal.Service.Base.PropertyVendorDistanceService, IPropertyVendorDistanceService
    {
        new protected IPropertyVendorDistanceRepository __repository;

        public PropertyVendorDistanceService()
            : this(new PropertyVendorDistanceRepository()) { }

        public PropertyVendorDistanceService(string connectionString)
            : this(new PropertyVendorDistanceRepository(connectionString)) { }

        public PropertyVendorDistanceService(IPropertyVendorDistanceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
