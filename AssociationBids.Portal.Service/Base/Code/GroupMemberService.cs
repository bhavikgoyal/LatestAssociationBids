using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class GroupMemberService : BaseService, IGroupMemberService
    {
        protected IGroupMemberRepository __repository;

        public GroupMemberService()
            : this(new GroupMemberRepository()) { }

        public GroupMemberService(string connectionString)
            : this(new GroupMemberRepository(connectionString)) { }

        public GroupMemberService(IGroupMemberRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(GroupMemberModel item)
        {
            if (!Util.IsValidInt(item.GroupKey))
            {
                AddError("GroupKey", "Group can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(GroupMemberFilterModel filter)
        {
            GroupMemberFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.GroupKey != filter.GroupKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            return false;
        }

        public virtual GroupMemberFilterModel CreateFilter()
        {
            GroupMemberFilterModel filter = new GroupMemberFilterModel();

            return UpdateFilter(filter);
        }

        public virtual GroupMemberFilterModel CreateFilter(GroupMemberModel item)
        {
            GroupMemberFilterModel filter = new GroupMemberFilterModel();

            filter.GroupKey = item.GroupKey;
            filter.ResourceKey = item.ResourceKey;

            return UpdateFilter(filter);
        }

        public virtual GroupMemberFilterModel UpdateFilter(GroupMemberFilterModel filter)
        {
            return filter;
        }

        public virtual GroupMemberModel Create()
        {
            ResetSiteSettings();

            GroupMemberModel item = new GroupMemberModel();

            return item;
        }

        public virtual bool Create(GroupMemberModel item)
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

        public virtual bool Update(GroupMemberModel item)
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

        public virtual GroupMemberModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<GroupMemberModel> GetAll()
        {
            GroupMemberFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<GroupMemberModel> GetAll(GroupMemberFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<GroupMemberModel> GetAll(GroupMemberFilterModel filter, PagingModel paging)
        {
            IList<GroupMemberModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
