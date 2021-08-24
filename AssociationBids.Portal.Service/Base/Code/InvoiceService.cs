using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        protected IInvoiceRepository __repository;

        public InvoiceService()
            : this(new InvoiceRepository()) { }

        public InvoiceService(string connectionString)
            : this(new InvoiceRepository(connectionString)) { }

        public InvoiceService(IInvoiceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(InvoiceModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidDateTime(item.TransactionDate))
            {
                AddError("TransactionDate", "Transaction Date can not be empty.");
            }
            if (!Util.IsValidDateTime(item.DueDate))
            {
                AddError("DueDate", "Due Date can not be empty.");
            }
            if (!Util.IsValidDecimal(item.Amount))
            {
                AddError("Amount", "Amount can not be empty.");
            }
            if (!Util.IsValidInt(item.Status))
            {
                AddError("Status", "Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(InvoiceFilterModel filter)
        {
            InvoiceFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual InvoiceFilterModel CreateFilter()
        {
            InvoiceFilterModel filter = new InvoiceFilterModel();

            if (SiteSettings.AdminPortal)
            {
                filter.Status = (int)LookUpType.RecordStatus.PendingApprovalOrApproved;
            }
            else
            {
                filter.Status = (int)LookUpType.RecordStatus.Approved;
            }

            return UpdateFilter(filter);
        }

        public virtual InvoiceFilterModel CreateFilter(InvoiceModel item)
        {
            InvoiceFilterModel filter = new InvoiceFilterModel();

            filter.VendorKey = item.VendorKey;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual InvoiceFilterModel UpdateFilter(InvoiceFilterModel filter)
        {
            return filter;
        }

        public virtual InvoiceModel Create()
        {
            ResetSiteSettings();

            InvoiceModel item = new InvoiceModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(InvoiceModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(InvoiceModel item)
        {
            item.LastModificationTime = DateTime.Now;

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

        public virtual InvoiceModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<InvoiceModel> GetAll()
        {
            InvoiceFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<InvoiceModel> GetAll(InvoiceFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<InvoiceModel> GetAll(InvoiceFilterModel filter, PagingModel paging)
        {
            IList<InvoiceModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
