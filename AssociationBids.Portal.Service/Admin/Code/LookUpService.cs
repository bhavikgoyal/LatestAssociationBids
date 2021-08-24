using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class LookUpService : AssociationBids.Portal.Service.Site.LookUpService, ILookUpService
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

        public override LookUpFilterModel UpdateFilter(LookUpFilterModel filter)
        {
            return filter;
        }
    }
}
