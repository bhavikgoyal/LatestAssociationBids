using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class InvoiceLineService : AssociationBids.Portal.Service.Base.InvoiceLineService, IInvoiceLineService
    {
        new protected IInvoiceLineRepository __repository;

        public InvoiceLineService()
            : this(new InvoiceLineRepository()) { }

        public InvoiceLineService(string connectionString)
            : this(new InvoiceLineRepository(connectionString)) { }

        public InvoiceLineService(IInvoiceLineRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
