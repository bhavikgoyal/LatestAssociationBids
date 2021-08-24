using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class InvoiceLineRepository : AssociationBids.Portal.Repository.Base.InvoiceLineRepository, IInvoiceLineRepository
    {
        public InvoiceLineRepository() 
            : base() { }

        public InvoiceLineRepository(string connectionString)
            : base(connectionString) { }
    }
}
