using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class UserService : AssociationBids.Portal.Service.Base.UserService, IUserService
    {
        new protected IUserRepository __repository;

        public UserService()
            : this(new UserRepository()) { }

        public UserService(string connectionString)
            : this(new UserRepository(connectionString)) { }

        public UserService(IUserRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
