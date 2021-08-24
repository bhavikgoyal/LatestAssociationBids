using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IGroupService : IBaseService
    {
        bool Validate(GroupModel item);
        bool IsFilterEnabled(GroupFilterModel filter);
        GroupFilterModel CreateFilter();
        GroupFilterModel CreateFilter(GroupModel item);
        GroupFilterModel UpdateFilter(GroupFilterModel filter);
        GroupModel Create();
        bool Create(GroupModel item);
        bool Update(GroupModel item);
        bool Delete(int id);
        GroupModel Get(int id);
        IList<GroupModel> GetAll();
        IList<GroupModel> GetAll(GroupFilterModel filter);
        IList<GroupModel> GetAll(GroupFilterModel filter, PagingModel paging);
    }
}
