using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class InvoiceService : AssociationBids.Portal.Service.Base.InvoiceService, IInvoiceService
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
    }
}
