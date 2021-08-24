using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class ResourceService : AssociationBids.Portal.Service.Site.ResourceService, IResourceService
    {
        new protected IResourceRepository __repository;

        public ResourceService()
            : this(new ResourceRepository()) { }

        public ResourceService(string connectionString)
            : this(new ResourceRepository(connectionString)) { }

        public ResourceService(IResourceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override ResourceFilterModel UpdateFilter(ResourceFilterModel filter)
        {
            return filter;
        }
    }
}
