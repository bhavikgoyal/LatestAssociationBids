using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class InvoiceLineService : AssociationBids.Portal.Service.Site.InvoiceLineService, IInvoiceLineService
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

        public override InvoiceLineFilterModel UpdateFilter(InvoiceLineFilterModel filter)
        {
            return filter;
        }
    }
}
