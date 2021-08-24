using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IGroupModuleAccessService : IBaseService
    {
        bool Validate(GroupModuleAccessModel item);
        bool IsFilterEnabled(GroupModuleAccessFilterModel filter);
        GroupModuleAccessFilterModel CreateFilter();
        GroupModuleAccessFilterModel CreateFilter(GroupModuleAccessModel item);
        GroupModuleAccessFilterModel UpdateFilter(GroupModuleAccessFilterModel filter);
        GroupModuleAccessModel Create();
        bool Create(GroupModuleAccessModel item);
        bool Update(GroupModuleAccessModel item);
        bool Delete(int id);
        GroupModuleAccessModel Get(int id);
        IList<GroupModuleAccessModel> GetAll();
        IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter);
        IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter, PagingModel paging);
    }
}
