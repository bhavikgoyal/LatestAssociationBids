using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class NoteRepository : AssociationBids.Portal.Repository.Site.NoteRepository, INoteRepository
    {
        public NoteRepository() 
            : base() { }

        public NoteRepository(string connectionString)
            : base(connectionString) { }
    }
}
