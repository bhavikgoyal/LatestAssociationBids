using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class NoteService : BaseService, INoteService
    {
        protected INoteRepository __repository;

        public NoteService()
            : this(new NoteRepository()) { }

        public NoteService(string connectionString)
            : this(new NoteRepository(connectionString)) { }

        public NoteService(INoteRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(NoteModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidText(item.Description))
            {
                AddError("Description", "Description can not be empty.");
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

        public virtual bool IsFilterEnabled(NoteFilterModel filter)
        {
            NoteFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.ObjectKey != filter.ObjectKey)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual NoteFilterModel CreateFilter()
        {
            NoteFilterModel filter = new NoteFilterModel();

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

        public virtual NoteFilterModel CreateFilter(NoteModel item)
        {
            NoteFilterModel filter = new NoteFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ResourceKey = item.ResourceKey;
            filter.ObjectKey = item.ObjectKey;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual NoteFilterModel UpdateFilter(NoteFilterModel filter)
        {
            return filter;
        }

        public virtual NoteModel Create()
        {
            ResetSiteSettings();

            NoteModel item = new NoteModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(NoteModel item)
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

        public virtual bool Update(NoteModel item)
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

        public virtual NoteModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<NoteModel> GetAll()
        {
            NoteFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<NoteModel> GetAll(NoteFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<NoteModel> GetAll(NoteFilterModel filter, PagingModel paging)
        {
            IList<NoteModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
