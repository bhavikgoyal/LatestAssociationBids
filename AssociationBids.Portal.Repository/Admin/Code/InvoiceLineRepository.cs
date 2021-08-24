using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class InvoiceLineRepository : AssociationBids.Portal.Repository.Site.InvoiceLineRepository, IInvoiceLineRepository
    {
        public InvoiceLineRepository() 
            : base() { }

        public InvoiceLineRepository(string connectionString)
            : base(connectionString) { }
    }
}
