using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class SessionService : AssociationBids.Portal.Service.Site.SessionService, ISessionService
    {
        new protected ISessionRepository __repository;

        public SessionService()
            : this(new SessionRepository()) { }

        public SessionService(string connectionString)
            : this(new SessionRepository(connectionString)) { }

        public SessionService(ISessionRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override SessionFilterModel UpdateFilter(SessionFilterModel filter)
        {
            return filter;
        }
    }
}
