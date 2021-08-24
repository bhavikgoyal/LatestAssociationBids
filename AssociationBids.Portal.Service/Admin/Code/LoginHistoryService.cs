using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class LoginHistoryService : AssociationBids.Portal.Service.Site.LoginHistoryService, ILoginHistoryService
    {
        new protected ILoginHistoryRepository __repository;

        public LoginHistoryService()
            : this(new LoginHistoryRepository()) { }

        public LoginHistoryService(string connectionString)
            : this(new LoginHistoryRepository(connectionString)) { }

        public LoginHistoryService(ILoginHistoryRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override LoginHistoryFilterModel UpdateFilter(LoginHistoryFilterModel filter)
        {
            return filter;
        }
    }
}
