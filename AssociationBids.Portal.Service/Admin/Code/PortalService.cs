using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class PortalService : AssociationBids.Portal.Service.Site.PortalService, IPortalService
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

        public override PortalFilterModel UpdateFilter(PortalFilterModel filter)
        {
            return filter;
        }
    }
}
