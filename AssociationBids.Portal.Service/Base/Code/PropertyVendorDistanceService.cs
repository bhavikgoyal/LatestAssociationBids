using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PropertyVendorDistanceService : BaseService, IPropertyVendorDistanceService
    {
        protected IPropertyVendorDistanceRepository __repository;

        public PropertyVendorDistanceService()
            : this(new PropertyVendorDistanceRepository()) { }

        public PropertyVendorDistanceService(string connectionString)
            : this(new PropertyVendorDistanceRepository(connectionString)) { }

        public PropertyVendorDistanceService(IPropertyVendorDistanceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PropertyVendorDistanceModel item)
        {
            if (!Util.IsValidInt(item.PropertyKey))
            {
                AddError("PropertyKey", "Property can not be empty.");
            }
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

        public virtual bool IsFilterEnabled(PropertyVendorDistanceFilterModel filter)
        {
            PropertyVendorDistanceFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.PropertyKey != filter.PropertyKey)
                return true;

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            return false;
        }

        public virtual PropertyVendorDistanceFilterModel CreateFilter()
        {
            PropertyVendorDistanceFilterModel filter = new PropertyVendorDistanceFilterModel();

            return UpdateFilter(filter);
        }

        public virtual PropertyVendorDistanceFilterModel CreateFilter(PropertyVendorDistanceModel item)
        {
            PropertyVendorDistanceFilterModel filter = new PropertyVendorDistanceFilterModel();

            filter.PropertyKey = item.PropertyKey;
            filter.VendorKey = item.VendorKey;

            return UpdateFilter(filter);
        }

        public virtual PropertyVendorDistanceFilterModel UpdateFilter(PropertyVendorDistanceFilterModel filter)
        {
            return filter;
        }

        public virtual PropertyVendorDistanceModel Create()
        {
            ResetSiteSettings();

            PropertyVendorDistanceModel item = new PropertyVendorDistanceModel();

            return item;
        }

        public virtual bool Create(PropertyVendorDistanceModel item)
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

        public virtual bool Update(PropertyVendorDistanceModel item)
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

        public virtual PropertyVendorDistanceModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PropertyVendorDistanceModel> GetAll()
        {
            PropertyVendorDistanceFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter, PagingModel paging)
        {
            IList<PropertyVendorDistanceModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
