using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class PropertyResourceService : AssociationBids.Portal.Service.Base.PropertyResourceService, IPropertyResourceService
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
    }
}
