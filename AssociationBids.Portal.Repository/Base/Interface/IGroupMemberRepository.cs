using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IGroupMemberRepository : IBaseRepository
    {
        bool Create(GroupMemberModel item);
        bool Update(GroupMemberModel item);
        bool Delete(int id);
        GroupMemberModel Get(int id);
        IList<GroupMemberModel> GetAll();
        IList<GroupMemberModel> GetAll(GroupMemberFilterModel filter);
        IList<GroupMemberModel> GetAll(GroupMemberFilterModel filter, PagingModel paging);
    }
}
