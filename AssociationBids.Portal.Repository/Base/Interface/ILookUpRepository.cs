using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ILookUpRepository : IBaseRepository
    {
        bool Create(LookUpModel item);
        bool Update(LookUpModel item);
        bool Delete(int id);
        LookUpModel Get(int id);
        IList<LookUpModel> GetAll();
        IList<LookUpModel> GetAll(LookUpFilterModel filter);
        IList<LookUpModel> GetAll(LookUpFilterModel filter, PagingModel paging);
    }
}
