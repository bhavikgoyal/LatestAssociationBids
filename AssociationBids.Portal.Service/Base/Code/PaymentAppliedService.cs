using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PaymentAppliedService : BaseService, IPaymentAppliedService
    {
        protected IPaymentAppliedRepository __repository;

        public PaymentAppliedService()
            : this(new PaymentAppliedRepository()) { }

        public PaymentAppliedService(string connectionString)
            : this(new PaymentAppliedRepository(connectionString)) { }

        public PaymentAppliedService(IPaymentAppliedRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PaymentAppliedModel item)
        {
            if (!Util.IsValidInt(item.PaymentKey))
            {
                AddError("PaymentKey", "Payment can not be empty.");
            }
            if (!Util.IsValidInt(item.InvociceKey))
            {
                AddError("InvociceKey", "Invocice can not be empty.");
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

        public virtual bool IsFilterEnabled(PaymentAppliedFilterModel filter)
        {
            PaymentAppliedFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.PaymentKey != filter.PaymentKey)
                return true;

            if (defaultFilter.InvociceKey != filter.InvociceKey)
                return true;

            return false;
        }

        public virtual PaymentAppliedFilterModel CreateFilter()
        {
            PaymentAppliedFilterModel filter = new PaymentAppliedFilterModel();

            return UpdateFilter(filter);
        }

        public virtual PaymentAppliedFilterModel CreateFilter(PaymentAppliedModel item)
        {
            PaymentAppliedFilterModel filter = new PaymentAppliedFilterModel();

            filter.PaymentKey = item.PaymentKey;
            filter.InvociceKey = item.InvociceKey;

            return UpdateFilter(filter);
        }

        public virtual PaymentAppliedFilterModel UpdateFilter(PaymentAppliedFilterModel filter)
        {
            return filter;
        }

        public virtual PaymentAppliedModel Create()
        {
            ResetSiteSettings();

            PaymentAppliedModel item = new PaymentAppliedModel();

            return item;
        }

        public virtual bool Create(PaymentAppliedModel item)
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

        public virtual bool Update(PaymentAppliedModel item)
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

        public virtual PaymentAppliedModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PaymentAppliedModel> GetAll()
        {
            PaymentAppliedFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter, PagingModel paging)
        {
            IList<PaymentAppliedModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
