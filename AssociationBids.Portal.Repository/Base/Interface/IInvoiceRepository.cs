using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IInvoiceRepository : IBaseRepository
    {
        bool Create(InvoiceModel item);
        bool Update(InvoiceModel item);
        bool Delete(int id);
        InvoiceModel Get(int id);
        IList<InvoiceModel> GetAll();
        IList<InvoiceModel> GetAll(InvoiceFilterModel filter);
        IList<InvoiceModel> GetAll(InvoiceFilterModel filter, PagingModel paging);
    }
}
