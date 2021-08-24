using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class InvoiceLineService : BaseService, IInvoiceLineService
    {
        protected IInvoiceLineRepository __repository;

        public InvoiceLineService()
            : this(new InvoiceLineRepository()) { }

        public InvoiceLineService(string connectionString)
            : this(new InvoiceLineRepository(connectionString)) { }

        public InvoiceLineService(IInvoiceLineRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(InvoiceLineModel item)
        {
            if (!Util.IsValidInt(item.InvoiceKey))
            {
                AddError("InvoiceKey", "Invoice can not be empty.");
            }
            if (!Util.IsValidDecimal(item.Amount))
            {
                AddError("Amount", "Amount can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(InvoiceLineFilterModel filter)
        {
            InvoiceLineFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.InvoiceKey != filter.InvoiceKey)
                return true;

            return false;
        }

        public virtual InvoiceLineFilterModel CreateFilter()
        {
            InvoiceLineFilterModel filter = new InvoiceLineFilterModel();

            return UpdateFilter(filter);
        }

        public virtual InvoiceLineFilterModel CreateFilter(InvoiceLineModel item)
        {
            InvoiceLineFilterModel filter = new InvoiceLineFilterModel();

            filter.InvoiceKey = item.InvoiceKey;

            return UpdateFilter(filter);
        }

        public virtual InvoiceLineFilterModel UpdateFilter(InvoiceLineFilterModel filter)
        {
            return filter;
        }

        public virtual InvoiceLineModel Create()
        {
            ResetSiteSettings();

            InvoiceLineModel item = new InvoiceLineModel();

            return item;
        }

        public virtual bool Create(InvoiceLineModel item)
        {
            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(InvoiceLineModel item)
        {
            if (Validate(item))
            {
                return __repository.Update(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            return __repository.Delete(id);
        }

        public virtual InvoiceLineModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<InvoiceLineModel> GetAll()
        {
            InvoiceLineFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter, PagingModel paging)
        {
            IList<InvoiceLineModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }
    }
}
