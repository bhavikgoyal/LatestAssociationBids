using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class UserRepository : AssociationBids.Portal.Repository.Base.UserRepository, IUserRepository
    {
        public UserRepository() 
            : base() { }

        public UserRepository(string connectionString)
            : base(connectionString) { }
    }
}
