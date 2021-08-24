using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IGroupRepository : IBaseRepository
    {
        bool Create(GroupModel item);
        bool Update(GroupModel item);
        bool Delete(int id);
        GroupModel Get(int id);
        IList<GroupModel> GetAll();
        IList<GroupModel> GetAll(GroupFilterModel filter);
        IList<GroupModel> GetAll(GroupFilterModel filter, PagingModel paging);
    }
}
