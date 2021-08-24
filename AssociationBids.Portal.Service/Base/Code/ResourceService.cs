using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class ResourceService : BaseService, IResourceService
    {
        protected IResourceRepository __repository;

        public ResourceService()
            : this(new ResourceRepository()) { }

        public ResourceService(string connectionString)
            : this(new ResourceRepository(connectionString)) { }

        public ResourceService(IResourceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(ResourceModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceTypeKey))
            {
                AddError("ResourceTypeKey", "Resource Type can not be empty.");
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

        public virtual bool IsFilterEnabled(ResourceFilterModel filter)
        {
            ResourceFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            if (defaultFilter.ResourceTypeKey != filter.ResourceTypeKey)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual ResourceFilterModel CreateFilter()
        {
            ResourceFilterModel filter = new ResourceFilterModel();

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
       
        public virtual ResourceFilterModel CreateFilter(ResourceModel item)
        {
            ResourceFilterModel filter = new ResourceFilterModel();

            filter.CompanyKey = item.CompanyKey;
            filter.ResourceTypeKey = item.ResourceTypeKey;
            filter.State = item.State;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual ResourceFilterModel UpdateFilter(ResourceFilterModel filter)
        {
            return filter;
        }

        public virtual ResourceModel Create()
        {
            ResetSiteSettings();

            ResourceModel item = new ResourceModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(ResourceModel item)
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

        public virtual bool Update(ResourceModel item)
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

        public virtual ResourceModel Get(int id)
        {
            return __repository.Get(id);
        }
        public virtual ResourceModel GetDataViewEditByCompanyKey(int CompanyKey)
        {
            return __repository.GetDataViewEditByCompanyKey(CompanyKey);
        }
        public virtual IList<ResourceModel> GetAll()
        {
            ResourceFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<ResourceModel> GetAll(ResourceFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<ResourceModel> GetAll(ResourceFilterModel filter, PagingModel paging)
        {
            IList<ResourceModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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

        public List<ResourceModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __repository.SearchUser(PageSize,PageIndex,Search,Sort);
        }

        public bool Insert(ResourceModel item)
        {
            return __repository.Insert(item);
        }

        public IList<ResourceModel> GetAllCompany()
        {
            return __repository.GetAllCompany();
        }

        public IList<ResourceModel> GetAllState()
        {
            return __repository.GetAllState();
        }


        public IList<VendorManagerModel> Getbindservice(int CompanyKey)
        {
            return __repository.Getbindservice(CompanyKey);
        }

        public IList<ResourceModel> GetServiceByCompany(int CompanyKey)
        {
            return __repository.GetServiceByCompany(CompanyKey);
        }
        public virtual ResourceModel GetDataViewEdit(int id)
        {
            return __repository.GetDataViewEdit(id);
        }
        public IList<ResourceModel> AppoGetAllService(string PleaseSelect)
        {
            return __repository.AppoGetAllService(PleaseSelect);
        }
        public virtual bool Edit(ResourceModel item)
        {
                item.LastModificationTime = DateTime.Now;
                return __repository.Edit(item);
           
        }

        public virtual bool PropertyMangerProfileEdit(ResourceModel item)
        {
            item.LastModificationTime = DateTime.Now;
            return __repository.PropertyMangerProfileEdit(item);

        }
        public virtual bool PropertyMangerProfileImage(Int32 ResourceKey, string Title, string Controller, string Action, string ImageName, Int64 ImageLength) {
            return __repository.PropertyMangerProfileImage(ResourceKey, Title, Controller, Action, ImageName, ImageLength);
        }

        public virtual string SaveProfilePassword(Int32 ResourceId, string OldPassword, string NewPassword)
        {
            return __repository.SaveProfilePassword(ResourceId,OldPassword,NewPassword);
        }

        public virtual bool Remove(int id)
        {
          
            return __repository.Remove(id);

        }
       


    }
}
