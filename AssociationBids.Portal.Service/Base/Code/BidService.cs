using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class BidService : BaseService, IBidService
    {
        protected IBidRepository __repository;

        public BidService()
            : this(new BidRepository()) { }

        public BidService(string connectionString)
            : this(new BidRepository(connectionString)) { }

        public BidService(IBidRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(BidModel item)
        {
            if (!Util.IsValidInt(item.BidVendorKey))
            {
                AddError("BidVendorKey", "Bid Vendor can not be empty.");
            }
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
            }
            if (!Util.IsValidInt(item.BidStatus))
            {
                AddError("BidStatus", "Bid Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(BidFilterModel filter)
        {
            BidFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.BidVendorKey != filter.BidVendorKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.BidStatus != filter.BidStatus)
                return true;

            return false;
        }

        public virtual BidFilterModel CreateFilter()
        {
            BidFilterModel filter = new BidFilterModel();

            return UpdateFilter(filter);
        }

        public virtual BidFilterModel CreateFilter(BidModel item)
        {
            BidFilterModel filter = new BidFilterModel();

            filter.BidVendorKey = item.BidVendorKey;
            filter.ResourceKey = item.ResourceKey;
            filter.BidStatus = item.BidStatus;

            return UpdateFilter(filter);
        }

        public virtual BidFilterModel UpdateFilter(BidFilterModel filter)
        {
            return filter;
        }

        public virtual BidModel Create()
        {
            ResetSiteSettings();

            BidModel item = new BidModel();

            return item;
        }

        public virtual bool Create(BidModel item)
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

        public virtual bool Update(BidModel item)
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

        public virtual BidModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<BidModel> GetAll()
        {
            BidFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<BidModel> GetAll(BidFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<BidModel> GetAll(BidFilterModel filter, PagingModel paging)
        {
            IList<BidModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
