using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class SessionRepository : AssociationBids.Portal.Repository.Site.SessionRepository, ISessionRepository
    {
        public SessionRepository() 
            : base() { }

        public SessionRepository(string connectionString)
            : base(connectionString) { }
    }
}
