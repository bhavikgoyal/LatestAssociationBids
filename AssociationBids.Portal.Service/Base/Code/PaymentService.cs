using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PaymentService : BaseService, IPaymentService
    {
        protected IPaymentRepository __repository;

        public PaymentService()
            : this(new PaymentRepository()) { }

        public PaymentService(string connectionString)
            : this(new PaymentRepository(connectionString)) { }

        public PaymentService(IPaymentRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PaymentModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidInt(item.PaymentTypeKey))
            {
                AddError("PaymentTypeKey", "Payment Type can not be empty.");
            }
            if (!Util.IsValidDateTime(item.TransactionDate))
            {
                AddError("TransactionDate", "Transaction Date can not be empty.");
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

        public virtual bool IsFilterEnabled(PaymentFilterModel filter)
        {
            PaymentFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.PaymentTypeKey != filter.PaymentTypeKey)
                return true;

            return false;
        }

        public virtual PaymentFilterModel CreateFilter()
        {
            PaymentFilterModel filter = new PaymentFilterModel();

            return UpdateFilter(filter);
        }

        public virtual PaymentFilterModel CreateFilter(PaymentModel item)
        {
            PaymentFilterModel filter = new PaymentFilterModel();

            filter.VendorKey = item.VendorKey;
            filter.PaymentTypeKey = item.PaymentTypeKey;

            return UpdateFilter(filter);
        }

        public virtual PaymentFilterModel UpdateFilter(PaymentFilterModel filter)
        {
            return filter;
        }

        public virtual PaymentModel Create()
        {
            ResetSiteSettings();

            PaymentModel item = new PaymentModel();

            return item;
        }

        public virtual bool Create(PaymentModel item)
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

        public virtual bool Update(PaymentModel item)
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

        public virtual PaymentModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PaymentModel> GetAll()
        {
            PaymentFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PaymentModel> GetAll(PaymentFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PaymentModel> GetAll(PaymentFilterModel filter, PagingModel paging)
        {
            IList<PaymentModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
