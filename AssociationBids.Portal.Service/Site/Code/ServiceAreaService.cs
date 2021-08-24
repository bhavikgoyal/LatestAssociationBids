using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class ServiceAreaService : AssociationBids.Portal.Service.Base.ServiceAreaService, IServiceAreaService
    {
        new protected IServiceAreaRepository __repository;

        public ServiceAreaService()
            : this(new ServiceAreaRepository()) { }

        public ServiceAreaService(string connectionString)
            : this(new ServiceAreaRepository(connectionString)) { }

        public ServiceAreaService(IServiceAreaRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
