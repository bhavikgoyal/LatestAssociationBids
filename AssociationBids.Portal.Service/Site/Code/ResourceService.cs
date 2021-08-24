using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class ResourceService : AssociationBids.Portal.Service.Base.ResourceService, IResourceService
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
    }
}
