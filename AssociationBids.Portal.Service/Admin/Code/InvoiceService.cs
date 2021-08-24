using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class InvoiceService : AssociationBids.Portal.Service.Site.InvoiceService, IInvoiceService
    {
        new protected IInvoiceRepository __repository;

        public InvoiceService()
            : this(new InvoiceRepository()) { }

        public InvoiceService(string connectionString)
            : this(new InvoiceRepository(connectionString)) { }

        public InvoiceService(IInvoiceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override InvoiceFilterModel UpdateFilter(InvoiceFilterModel filter)
        {
            return filter;
        }
    }
}
