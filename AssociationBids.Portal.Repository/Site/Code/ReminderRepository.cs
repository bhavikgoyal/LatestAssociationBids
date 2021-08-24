using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class ReminderRepository : AssociationBids.Portal.Repository.Base.ReminderRepository, IReminderRepository
    {
        public ReminderRepository() 
            : base() { }

        public ReminderRepository(string connectionString)
            : base(connectionString) { }
    }
}
