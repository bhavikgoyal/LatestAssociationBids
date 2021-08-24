using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class LoginHistoryRepository : AssociationBids.Portal.Repository.Base.LoginHistoryRepository, ILoginHistoryRepository
    {
        public LoginHistoryRepository() 
            : base() { }

        public LoginHistoryRepository(string connectionString)
            : base(connectionString) { }
    }
}
