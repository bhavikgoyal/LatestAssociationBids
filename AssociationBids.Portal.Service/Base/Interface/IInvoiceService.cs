using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IInvoiceService : IBaseService
    {
        bool Validate(InvoiceModel item);
        bool IsFilterEnabled(InvoiceFilterModel filter);
        InvoiceFilterModel CreateFilter();
        InvoiceFilterModel CreateFilter(InvoiceModel item);
        InvoiceFilterModel UpdateFilter(InvoiceFilterModel filter);
        InvoiceModel Create();
        bool Create(InvoiceModel item);
        bool Update(InvoiceModel item);
        bool Delete(int id);
        InvoiceModel Get(int id);
        IList<InvoiceModel> GetAll();
        IList<InvoiceModel> GetAll(InvoiceFilterModel filter);
        IList<InvoiceModel> GetAll(InvoiceFilterModel filter, PagingModel paging);
    }
}
