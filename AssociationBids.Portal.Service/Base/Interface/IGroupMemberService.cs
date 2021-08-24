using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IGroupMemberService : IBaseService
    {
        bool Validate(GroupMemberModel item);
        bool IsFilterEnabled(GroupMemberFilterModel filter);
        GroupMemberFilterModel CreateFilter();
        GroupMemberFilterModel CreateFilter(GroupMemberModel item);
        GroupMemberFilterModel UpdateFilter(GroupMemberFilterModel filter);
        GroupMemberModel Create();
        bool Create(GroupMemberModel item);
        bool Update(GroupMemberModel item);
        bool Delete(int id);
        GroupMemberModel Get(int id);
        IList<GroupMemberModel> GetAll();
        IList<GroupMemberModel> GetAll(GroupMemberFilterModel filter);
        IList<GroupMemberModel> GetAll(GroupMemberFilterModel filter, PagingModel paging);
    }
}
