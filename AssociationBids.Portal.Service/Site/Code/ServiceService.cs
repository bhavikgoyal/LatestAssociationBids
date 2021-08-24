using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class ServiceService : AssociationBids.Portal.Service.Base.ServiceService, IServiceService
    {
        new protected IServiceRepository __repository;

        public ServiceService()
            : this(new ServiceRepository()) { }

        public ServiceService(string connectionString)
            : this(new ServiceRepository(connectionString)) { }

        public ServiceService(IServiceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
