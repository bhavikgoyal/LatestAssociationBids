using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class EmailRepository : AssociationBids.Portal.Repository.Site.EmailRepository, IEmailRepository
    {
        public EmailRepository() 
            : base() { }

        public EmailRepository(string connectionString)
            : base(connectionString) { }
    }
}
