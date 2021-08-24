using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class UserService : AssociationBids.Portal.Service.Site.UserService, IUserService
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

        public override UserFilterModel UpdateFilter(UserFilterModel filter)
        {
            return filter;
        }
    }
}
