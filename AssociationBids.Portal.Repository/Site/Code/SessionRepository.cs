using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class SessionRepository : AssociationBids.Portal.Repository.Base.SessionRepository, ISessionRepository
    {
        public SessionRepository() 
            : base() { }

        public SessionRepository(string connectionString)
            : base(connectionString) { }
    }
}
