using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PricingService : BaseService, IPricingService
    {
        protected IPricingRepository __repository;

        public PricingService()
            : this(new PricingRepository()) { }

        public PricingService(string connectionString)
            : this(new PricingRepository(connectionString)) { }

        public PricingService(IPricingRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PricingModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidInt(item.PricingTypeKey))
            {
                AddError("PricingTypeKey", "Pricing Type can not be empty.");
            }
            if (!Util.IsValidDecimal(item.Fee))
            {
                AddError("Fee", "Fee can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(PricingFilterModel filter)
        {
            PricingFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            if (defaultFilter.PricingTypeKey != filter.PricingTypeKey)
                return true;

            return false;
        }

        public virtual PricingFilterModel CreateFilter()
        {
            PricingFilterModel filter = new PricingFilterModel();

            return UpdateFilter(filter);
        }

        public virtual PricingFilterModel CreateFilter(PricingModel item)
        {
            PricingFilterModel filter = new PricingFilterModel();

            filter.CompanyKey = item.CompanyKey;
            filter.PricingTypeKey = item.PricingTypeKey;

            return UpdateFilter(filter);
        }

        public virtual PricingFilterModel UpdateFilter(PricingFilterModel filter)
        {
            return filter;
        }

        public virtual PricingModel Create()
        {
            ResetSiteSettings();

            PricingModel item = new PricingModel();

            return item;
        }

        public virtual bool Create(PricingModel item)
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

        public virtual bool Update(PricingModel item)
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

        public virtual PricingModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PricingModel> GetAll()
        {
            PricingFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PricingModel> GetAll(PricingFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PricingModel> GetAll(PricingFilterModel filter, PagingModel paging)
        {
            IList<PricingModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
