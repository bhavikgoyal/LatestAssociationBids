using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class CompanyVendorService : BaseService, ICompanyVendorService
    {
        protected ICompanyVendorRepository __repository;

        public CompanyVendorService()
            : this(new CompanyVendorRepository()) { }

        public CompanyVendorService(string connectionString)
            : this(new CompanyVendorRepository(connectionString)) { }

        public CompanyVendorService(ICompanyVendorRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(CompanyVendorModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidInt(item.Status))
            {
                AddError("Status", "Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(CompanyVendorFilterModel filter)
        {
            CompanyVendorFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual CompanyVendorFilterModel CreateFilter()
        {
            CompanyVendorFilterModel filter = new CompanyVendorFilterModel();

            if (SiteSettings.AdminPortal)
            {
                filter.Status = (int)LookUpType.RecordStatus.PendingApprovalOrApproved;
            }
            else
            {
                filter.Status = (int)LookUpType.RecordStatus.Approved;
            }

            return UpdateFilter(filter);
        }

        public virtual CompanyVendorFilterModel CreateFilter(CompanyVendorModel item)
        {
            CompanyVendorFilterModel filter = new CompanyVendorFilterModel();

            filter.CompanyKey = item.CompanyKey;
            filter.VendorKey = item.VendorKey;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual CompanyVendorFilterModel UpdateFilter(CompanyVendorFilterModel filter)
        {
            return filter;
        }

        public virtual CompanyVendorModel Create()
        {
            ResetSiteSettings();

            CompanyVendorModel item = new CompanyVendorModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(CompanyVendorModel item)
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

        public virtual bool Update(CompanyVendorModel item)
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

        public virtual CompanyVendorModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<CompanyVendorModel> GetAll()
        {
            CompanyVendorFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<CompanyVendorModel> GetAll(CompanyVendorFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<CompanyVendorModel> GetAll(CompanyVendorFilterModel filter, PagingModel paging)
        {
            IList<CompanyVendorModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
