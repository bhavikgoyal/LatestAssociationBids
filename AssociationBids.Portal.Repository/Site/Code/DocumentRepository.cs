using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class DocumentRepository : AssociationBids.Portal.Repository.Base.DocumentRepository, IDocumentRepository
    {
        public DocumentRepository() 
            : base() { }

        public DocumentRepository(string connectionString)
            : base(connectionString) { }
    }
}
