using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class UserRepository : AssociationBids.Portal.Repository.Site.UserRepository, IUserRepository
    {
        public UserRepository() 
            : base() { }

        public UserRepository(string connectionString)
            : base(connectionString) { }
    }
}
