using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class CompanyService : BaseService, ICompanyService
    {
        protected ICompanyRepository __repository;

        public CompanyService()
            : this(new CompanyRepository()) { }

        public CompanyService(string connectionString)
            : this(new CompanyRepository(connectionString)) { }

        public CompanyService(ICompanyRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(CompanyModel item)
        {
            if (!Util.IsValidInt(item.CompanyTypeKey))
            {
                AddError("CompanyTypeKey", "Company Type can not be empty.");
            }
            if (!Util.IsValidText(item.Name))
            {
                AddError("Name", "Name can not be empty.");
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

        public virtual bool IsFilterEnabled(CompanyFilterModel filter)
        {
            CompanyFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ParentCompanyKey != filter.ParentCompanyKey)
                return true;

            if (defaultFilter.RelatedCompanyKey != filter.RelatedCompanyKey)
                return true;

            if (defaultFilter.CompanyTypeKey != filter.CompanyTypeKey)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            if (defaultFilter.Name != filter.Name)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual CompanyFilterModel CreateFilter()
        {
            CompanyFilterModel filter = new CompanyFilterModel();

            filter.PortalKey = SiteSettings.CurrentPortalKey;
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

        public virtual CompanyFilterModel CreateFilter(CompanyModel item)
        {
            CompanyFilterModel filter = new CompanyFilterModel();

            filter.ParentCompanyKey = item.ParentCompanyKey;
            filter.RelatedCompanyKey = item.RelatedCompanyKey;
            filter.CompanyTypeKey = item.CompanyTypeKey;
            filter.PortalKey = item.PortalKey;
            filter.State = item.State;
            filter.Name = item.Name;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual CompanyFilterModel UpdateFilter(CompanyFilterModel filter)
        {
            return filter;
        }

        public virtual CompanyModel Create()
        {
            ResetSiteSettings();

            CompanyModel item = new CompanyModel();

            item.PortalKey = SiteSettings.CurrentPortalKey;
            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(CompanyModel item)
        {
            item.DateAdded = DateTime.Now;
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                UpdateSiteSettings(item.PortalKey);

                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(CompanyModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                UpdateSiteSettings(item.PortalKey);

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

        public virtual CompanyModel Get(int id)
        {
            CompanyModel item = __repository.Get(id);

            UpdateSiteSettings(item.PortalKey);

            return item;
        }

        public virtual IList<CompanyModel> GetAll()
        {
            CompanyFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<CompanyModel> GetAll(CompanyFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<CompanyModel> GetAll(CompanyFilterModel filter, PagingModel paging)
        {
            IList<CompanyModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
