using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IStateRepository : IBaseRepository
    {
        bool Create(StateModel item);
        bool Update(StateModel item);
        bool Delete(string id);
        StateModel Get(string id);
        IList<StateModel> GetAll();
        IList<StateModel> GetAll(StateFilterModel filter);
        IList<StateModel> GetAll(StateFilterModel filter, PagingModel paging);
    }
}
