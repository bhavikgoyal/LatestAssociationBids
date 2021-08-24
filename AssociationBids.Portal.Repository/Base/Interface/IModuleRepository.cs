using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IModuleRepository : IBaseRepository
    {
        bool Create(ModuleModel item);
        bool Update(ModuleModel item);
        bool Delete(int id);
        ModuleModel Get(int id);
        IList<ModuleModel> GetAll();
        IList<ModuleModel> GetAll(ModuleFilterModel filter);
        IList<ModuleModel> GetAll(ModuleFilterModel filter, PagingModel paging);
    }
}
