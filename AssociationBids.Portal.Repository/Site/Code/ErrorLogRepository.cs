using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class ErrorLogRepository : AssociationBids.Portal.Repository.Base.ErrorLogRepository, IErrorLogRepository
    {
        public ErrorLogRepository() 
            : base() { }

        public ErrorLogRepository(string connectionString)
            : base(connectionString) { }
    }
}
