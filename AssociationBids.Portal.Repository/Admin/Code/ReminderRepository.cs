using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class ReminderRepository : AssociationBids.Portal.Repository.Site.ReminderRepository, IReminderRepository
    {
        public ReminderRepository() 
            : base() { }

        public ReminderRepository(string connectionString)
            : base(connectionString) { }
    }
}
