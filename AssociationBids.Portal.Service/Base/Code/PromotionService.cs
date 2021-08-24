using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PromotionService : BaseService, IPromotionService
    {
        protected IPromotionRepository __repository;

        public PromotionService()
            : this(new PromotionRepository()) { }

        public PromotionService(string connectionString)
            : this(new PromotionRepository(connectionString)) { }

        public PromotionService(IPromotionRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PromotionModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
            }
            if (!Util.IsValidText(item.PromotionCode))
            {
                AddError("PromotionCode", "Promotion Code can not be empty.");
            }
            if (!Util.IsValidDouble(item.Percentage))
            {
                AddError("Percentage", "Percentage can not be empty.");
            }
            if (!Util.IsValidDateTime(item.StartDate))
            {
                AddError("StartDate", "Start Date can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(PromotionFilterModel filter)
        {
            PromotionFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            return false;
        }

        public virtual PromotionFilterModel CreateFilter()
        {
            PromotionFilterModel filter = new PromotionFilterModel();

            return UpdateFilter(filter);
        }

        public virtual PromotionFilterModel CreateFilter(PromotionModel item)
        {
            PromotionFilterModel filter = new PromotionFilterModel();

            filter.CompanyKey = item.CompanyKey;

            return UpdateFilter(filter);
        }

        public virtual PromotionFilterModel UpdateFilter(PromotionFilterModel filter)
        {
            return filter;
        }

        public virtual PromotionModel Create()
        {
            ResetSiteSettings();

            PromotionModel item = new PromotionModel();

            return item;
        }

        public virtual bool Create(PromotionModel item)
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

        public virtual bool Update(PromotionModel item)
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

        public virtual PromotionModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PromotionModel> GetAll()
        {
            PromotionFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PromotionModel> GetAll(PromotionFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PromotionModel> GetAll(PromotionFilterModel filter, PagingModel paging)
        {
            IList<PromotionModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
