using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class GroupService : BaseService, IGroupService
    {
        protected IGroupRepository __repository;

        public GroupService()
            : this(new GroupRepository()) { }

        public GroupService(string connectionString)
            : this(new GroupRepository(connectionString)) { }

        public GroupService(IGroupRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(GroupModel item)
        {
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(GroupFilterModel filter)
        {
            GroupFilterModel defaultFilter = CreateFilter();

            return false;
        }

        public virtual GroupFilterModel CreateFilter()
        {
            GroupFilterModel filter = new GroupFilterModel();

            return UpdateFilter(filter);
        }

        public virtual GroupFilterModel CreateFilter(GroupModel item)
        {
            GroupFilterModel filter = new GroupFilterModel();
            return UpdateFilter(filter);
        }

        public virtual GroupFilterModel UpdateFilter(GroupFilterModel filter)
        {
            return filter;
        }

        public virtual GroupModel Create()
        {
            ResetSiteSettings();

            GroupModel item = new GroupModel();

            return item;
        }

        public virtual bool Create(GroupModel item)
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

        public virtual bool Update(GroupModel item)
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

        public virtual GroupModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<GroupModel> GetAll()
        {
            GroupFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<GroupModel> GetAll(GroupFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<GroupModel> GetAll(GroupFilterModel filter, PagingModel paging)
        {
            IList<GroupModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
