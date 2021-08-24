using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ILookUpTypeRepository : IBaseRepository
    {
        bool Create(LookUpTypeModel item);
        bool Update(LookUpTypeModel item);
        bool Delete(int id);
        LookUpTypeModel Get(int id);
        IList<LookUpTypeModel> GetAll();
        IList<LookUpTypeModel> GetAll(LookUpTypeFilterModel filter);
        IList<LookUpTypeModel> GetAll(LookUpTypeFilterModel filter, PagingModel paging);
    }
}
