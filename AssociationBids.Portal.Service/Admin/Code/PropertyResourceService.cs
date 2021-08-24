using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class PropertyResourceService : AssociationBids.Portal.Service.Site.PropertyResourceService, IPropertyResourceService
    {
        new protected IPropertyResourceRepository __repository;

        public PropertyResourceService()
            : this(new PropertyResourceRepository()) { }

        public PropertyResourceService(string connectionString)
            : this(new PropertyResourceRepository(connectionString)) { }

        public PropertyResourceService(IPropertyResourceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override PropertyResourceFilterModel UpdateFilter(PropertyResourceFilterModel filter)
        {
            return filter;
        }
    }
}
