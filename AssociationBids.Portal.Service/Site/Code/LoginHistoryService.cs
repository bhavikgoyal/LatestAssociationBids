using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class LoginHistoryService : AssociationBids.Portal.Service.Base.LoginHistoryService, ILoginHistoryService
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
    }
}
