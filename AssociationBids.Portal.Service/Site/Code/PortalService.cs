using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class PortalService : AssociationBids.Portal.Service.Base.PortalService, IPortalService
    {
        new protected IPortalRepository __repository;

        public PortalService()
            : this(new PortalRepository()) { }

        public PortalService(string connectionString)
            : this(new PortalRepository(connectionString)) { }

        public PortalService(IPortalRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
