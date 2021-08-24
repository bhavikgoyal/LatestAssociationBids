using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IGroupModuleAccessRepository : IBaseRepository
    {
        bool Create(GroupModuleAccessModel item);
        bool Update(GroupModuleAccessModel item);
        bool Delete(int id);
        GroupModuleAccessModel Get(int id);
        IList<GroupModuleAccessModel> GetAll();
        IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter);
        IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter, PagingModel paging);
    }
}
