using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IStateService : IBaseService
    {
        bool Validate(StateModel item);
        bool IsFilterEnabled(StateFilterModel filter);
        StateFilterModel CreateFilter();
        StateFilterModel CreateFilter(StateModel item);
        StateFilterModel UpdateFilter(StateFilterModel filter);
        StateModel Create();
        bool Create(StateModel item);
        bool Update(StateModel item);
        bool Delete(string id);
        StateModel Get(string id);
        IList<StateModel> GetAll();
        IList<StateModel> GetAll(StateFilterModel filter);
        IList<StateModel> GetAll(StateFilterModel filter, PagingModel paging);
    }
}
