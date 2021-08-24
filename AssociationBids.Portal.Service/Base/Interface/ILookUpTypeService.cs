using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ILookUpTypeService : IBaseService
    {
        bool Validate(LookUpTypeModel item);
        bool IsFilterEnabled(LookUpTypeFilterModel filter);
        LookUpTypeFilterModel CreateFilter();
        LookUpTypeFilterModel CreateFilter(LookUpTypeModel item);
        LookUpTypeFilterModel UpdateFilter(LookUpTypeFilterModel filter);
        LookUpTypeModel Create();
        bool Create(LookUpTypeModel item);
        bool Update(LookUpTypeModel item);
        bool Delete(int id);
        LookUpTypeModel Get(int id);
        IList<LookUpTypeModel> GetAll();
        IList<LookUpTypeModel> GetAll(LookUpTypeFilterModel filter);
        IList<LookUpTypeModel> GetAll(LookUpTypeFilterModel filter, PagingModel paging);
    }
}
