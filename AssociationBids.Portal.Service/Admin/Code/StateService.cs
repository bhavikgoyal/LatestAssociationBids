using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class StateService : AssociationBids.Portal.Service.Site.StateService, IStateService
    {
        new protected IStateRepository __repository;

        public StateService()
            : this(new StateRepository()) { }

        public StateService(string connectionString)
            : this(new StateRepository(connectionString)) { }

        public StateService(IStateRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override StateFilterModel UpdateFilter(StateFilterModel filter)
        {
            return filter;
        }
    }
}
