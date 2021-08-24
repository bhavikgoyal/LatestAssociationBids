using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class VendorServiceService : BaseService, IVendorServiceService
    {
        protected IVendorServiceRepository __repository;

        public VendorServiceService()
            : this(new VendorServiceRepository()) { }

        public VendorServiceService(string connectionString)
            : this(new VendorServiceRepository(connectionString)) { }

        public VendorServiceService(IVendorServiceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(VendorServiceModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidInt(item.ServiceKey))
            {
                AddError("ServiceKey", "Service can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(VendorServiceFilterModel filter)
        {
            VendorServiceFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.ServiceKey != filter.ServiceKey)
                return true;

            return false;
        }

        public virtual VendorServiceFilterModel CreateFilter()
        {
            VendorServiceFilterModel filter = new VendorServiceFilterModel();

            return UpdateFilter(filter);
        }

        public virtual VendorServiceFilterModel CreateFilter(VendorServiceModel item)
        {
            VendorServiceFilterModel filter = new VendorServiceFilterModel();

            filter.VendorKey = item.VendorKey;
            filter.ServiceKey = item.ServiceKey;

            return UpdateFilter(filter);
        }

        public virtual VendorServiceFilterModel UpdateFilter(VendorServiceFilterModel filter)
        {
            return filter;
        }

        public virtual VendorServiceModel Create()
        {
            ResetSiteSettings();

            VendorServiceModel item = new VendorServiceModel();

            return item;
        }

        public virtual bool Create(VendorServiceModel item)
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

        public virtual bool Update(VendorServiceModel item)
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

        public virtual VendorServiceModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<VendorServiceModel> GetAll()
        {
            VendorServiceFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter, PagingModel paging)
        {
            IList<VendorServiceModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
