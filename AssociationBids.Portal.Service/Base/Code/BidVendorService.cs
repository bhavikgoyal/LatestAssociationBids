using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class BidVendorService : BaseService, IBidVendorService
    {
        protected IBidVendorRepository __repository;

        public BidVendorService()
            : this(new BidVendorRepository()) { }

        public BidVendorService(string connectionString)
            : this(new BidVendorRepository(connectionString)) { }

        public BidVendorService(IBidVendorRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(BidVendorModel item)
        {
            if (!Util.IsValidInt(item.BidRequestKey))
            {
                AddError("BidRequestKey", "Bid Request can not be empty.");
            }
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidText(item.BidVendorID))
            {
                AddError("BidVendorID", "Bid Vendor can not be empty.");
            }
            if (!Util.IsValidInt(item.BidVendorStatus))
            {
                AddError("BidVendorStatus", "Bid Vendor Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(BidVendorFilterModel filter)
        {
            BidVendorFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.BidRequestKey != filter.BidRequestKey)
                return true;

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.BidVendorStatus != filter.BidVendorStatus)
                return true;

            return false;
        }

        public virtual BidVendorFilterModel CreateFilter()
        {
            BidVendorFilterModel filter = new BidVendorFilterModel();

            return UpdateFilter(filter);
        }

        public virtual BidVendorFilterModel CreateFilter(BidVendorModel item)
        {
            BidVendorFilterModel filter = new BidVendorFilterModel();

            filter.BidRequestKey = item.BidRequestKey;
            filter.VendorKey = item.VendorKey;
            filter.ResourceKey = item.ResourceKey;
            filter.BidVendorStatus = item.BidVendorStatus;

            return UpdateFilter(filter);
        }

        public virtual BidVendorFilterModel UpdateFilter(BidVendorFilterModel filter)
        {
            return filter;
        }

        public virtual BidVendorModel Create()
        {
            ResetSiteSettings();

            BidVendorModel item = new BidVendorModel();

            return item;
        }

        public virtual bool Create(BidVendorModel item)
        {
            item.DateAdded = DateTime.Now;
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

        public virtual bool Update(BidVendorModel item)
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

        public virtual BidVendorModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<BidVendorModel> GetAll()
        {
            BidVendorFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<BidVendorModel> GetAll(BidVendorFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<BidVendorModel> GetAll(BidVendorFilterModel filter, PagingModel paging)
        {
            IList<BidVendorModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
