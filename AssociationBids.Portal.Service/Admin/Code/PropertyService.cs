using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class PropertyService : AssociationBids.Portal.Service.Site.PropertyService, IPropertyService
    {
        new protected IPropertyRepository __repository;

        public PropertyService()
            : this(new PropertyRepository()) { }

        public PropertyService(string connectionString)
            : this(new PropertyRepository(connectionString)) { }

        public PropertyService(IPropertyRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override PropertyFilterModel UpdateFilter(PropertyFilterModel filter)
        {
            return filter;
        }
    }
}
