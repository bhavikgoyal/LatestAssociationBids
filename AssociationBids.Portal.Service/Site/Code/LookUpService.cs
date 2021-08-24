using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class LookUpService : AssociationBids.Portal.Service.Base.LookUpService, ILookUpService
    {
        new protected ILookUpRepository __repository;

        public LookUpService()
            : this(new LookUpRepository()) { }

        public LookUpService(string connectionString)
            : this(new LookUpRepository(connectionString)) { }

        public LookUpService(ILookUpRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
