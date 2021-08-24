using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IInvoiceLineService : IBaseService
    {
        bool Validate(InvoiceLineModel item);
        bool IsFilterEnabled(InvoiceLineFilterModel filter);
        InvoiceLineFilterModel CreateFilter();
        InvoiceLineFilterModel CreateFilter(InvoiceLineModel item);
        InvoiceLineFilterModel UpdateFilter(InvoiceLineFilterModel filter);
        InvoiceLineModel Create();
        bool Create(InvoiceLineModel item);
        bool Update(InvoiceLineModel item);
        bool Delete(int id);
        InvoiceLineModel Get(int id);
        IList<InvoiceLineModel> GetAll();
        IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter);
        IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter, PagingModel paging);
    }
}
