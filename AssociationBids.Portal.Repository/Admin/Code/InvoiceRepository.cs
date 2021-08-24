using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class InvoiceRepository : AssociationBids.Portal.Repository.Site.InvoiceRepository, IInvoiceRepository
    {
        public InvoiceRepository() 
            : base() { }

        public InvoiceRepository(string connectionString)
            : base(connectionString) { }
    }
}
