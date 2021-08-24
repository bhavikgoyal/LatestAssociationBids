using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ILookUpService : IBaseService
    {
        bool Validate(LookUpModel item);
        bool IsFilterEnabled(LookUpFilterModel filter);
        LookUpFilterModel CreateFilter();
        LookUpFilterModel CreateFilter(LookUpModel item);
        LookUpFilterModel UpdateFilter(LookUpFilterModel filter);
        LookUpModel Create();
        bool Create(LookUpModel item);
        bool Update(LookUpModel item);
        bool Delete(int id);
        LookUpModel Get(int id);
        IList<LookUpModel> GetAll();
        IList<LookUpModel> GetAll(LookUpFilterModel filter);
        IList<LookUpModel> GetAll(LookUpFilterModel filter, PagingModel paging);
    }
}
