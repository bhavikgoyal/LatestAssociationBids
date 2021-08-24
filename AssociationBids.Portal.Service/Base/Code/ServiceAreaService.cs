using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class ServiceAreaService : BaseService, IServiceAreaService
    {
        protected IServiceAreaRepository __repository;

        public ServiceAreaService()
            : this(new ServiceAreaRepository()) { }

        public ServiceAreaService(string connectionString)
            : this(new ServiceAreaRepository(connectionString)) { }

        public ServiceAreaService(IServiceAreaRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(ServiceAreaModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidText(item.Address))
            {
                AddError("Address", "Address can not be empty.");
            }
            if (!Util.IsValidText(item.City))
            {
                AddError("City", "City can not be empty.");
            }
            if (!Util.IsValidText(item.State))
            {
                AddError("State", "State can not be empty.");
            }
            if (!Util.IsValidText(item.Zip))
            {
                AddError("Zip", "Zip can not be empty.");
            }
            if (!Util.IsValidInt(item.Radius))
            {
                AddError("Radius", "Radius can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(ServiceAreaFilterModel filter)
        {
            ServiceAreaFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            return false;
        }

        public virtual ServiceAreaFilterModel CreateFilter()
        {
            ServiceAreaFilterModel filter = new ServiceAreaFilterModel();

            return UpdateFilter(filter);
        }

        public virtual ServiceAreaFilterModel CreateFilter(ServiceAreaModel item)
        {
            ServiceAreaFilterModel filter = new ServiceAreaFilterModel();

            filter.VendorKey = item.VendorKey;
            filter.State = item.State;

            return UpdateFilter(filter);
        }

        public virtual ServiceAreaFilterModel UpdateFilter(ServiceAreaFilterModel filter)
        {
            return filter;
        }

        public virtual ServiceAreaModel Create()
        {
            ResetSiteSettings();

            ServiceAreaModel item = new ServiceAreaModel();

            return item;
        }

        public virtual bool Create(ServiceAreaModel item)
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

        public virtual bool Update(ServiceAreaModel item)
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

        public virtual ServiceAreaModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<ServiceAreaModel> GetAll()
        {
            ServiceAreaFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter, PagingModel paging)
        {
            IList<ServiceAreaModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
