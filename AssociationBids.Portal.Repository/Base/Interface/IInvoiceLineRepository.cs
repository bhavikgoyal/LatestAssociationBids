using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IInvoiceLineRepository : IBaseRepository
    {
        bool Create(InvoiceLineModel item);
        bool Update(InvoiceLineModel item);
        bool Delete(int id);
        InvoiceLineModel Get(int id);
        IList<InvoiceLineModel> GetAll();
        IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter);
        IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter, PagingModel paging);
    }
}
