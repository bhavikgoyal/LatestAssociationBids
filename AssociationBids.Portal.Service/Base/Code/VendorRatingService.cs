using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class VendorRatingService : BaseService, IVendorRatingService
    {
        protected IVendorRatingRepository __repository;

        public VendorRatingService()
            : this(new VendorRatingRepository()) { }

        public VendorRatingService(string connectionString)
            : this(new VendorRatingRepository(connectionString)) { }

        public VendorRatingService(IVendorRatingRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(VendorRatingModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(VendorRatingFilterModel filter)
        {
            VendorRatingFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            return false;
        }

        public virtual VendorRatingFilterModel CreateFilter()
        {
            VendorRatingFilterModel filter = new VendorRatingFilterModel();

            return UpdateFilter(filter);
        }

        public virtual VendorRatingFilterModel CreateFilter(VendorRatingModel item)
        {
            VendorRatingFilterModel filter = new VendorRatingFilterModel();

            filter.VendorKey = item.VendorKey;
            filter.ResourceKey = item.ResourceKey;

            return UpdateFilter(filter);
        }

        public virtual VendorRatingFilterModel UpdateFilter(VendorRatingFilterModel filter)
        {
            return filter;
        }

        public virtual VendorRatingModel Create()
        {
            ResetSiteSettings();

            VendorRatingModel item = new VendorRatingModel();

            return item;
        }

        public virtual bool Create(VendorRatingModel item)
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

        public virtual bool Update(VendorRatingModel item)
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

        public virtual VendorRatingModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<VendorRatingModel> GetAll()
        {
            VendorRatingFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter, PagingModel paging)
        {
            IList<VendorRatingModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
