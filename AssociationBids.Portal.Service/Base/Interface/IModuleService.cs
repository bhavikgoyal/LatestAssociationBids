using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IModuleService : IBaseService
    {
        bool Validate(ModuleModel item);
        bool IsFilterEnabled(ModuleFilterModel filter);
        ModuleFilterModel CreateFilter();
        ModuleFilterModel CreateFilter(ModuleModel item);
        ModuleFilterModel UpdateFilter(ModuleFilterModel filter);
        ModuleModel Create();
        bool Create(ModuleModel item);
        bool Update(ModuleModel item);
        bool Delete(int id);
        ModuleModel Get(int id);
        IList<ModuleModel> GetAll();
        IList<ModuleModel> GetAll(ModuleFilterModel filter);
        IList<ModuleModel> GetAll(ModuleFilterModel filter, PagingModel paging);
    }
}
