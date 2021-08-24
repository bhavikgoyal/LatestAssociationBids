using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class InvoiceRepository : AssociationBids.Portal.Repository.Base.InvoiceRepository, IInvoiceRepository
    {
        public InvoiceRepository() 
            : base() { }

        public InvoiceRepository(string connectionString)
            : base(connectionString) { }
    }
}
