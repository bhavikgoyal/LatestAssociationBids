using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class MessageRepository : AssociationBids.Portal.Repository.Site.MessageRepository, IMessageRepository
    {
        public MessageRepository() 
            : base() { }

        public MessageRepository(string connectionString)
            : base(connectionString) { }
    }
}
