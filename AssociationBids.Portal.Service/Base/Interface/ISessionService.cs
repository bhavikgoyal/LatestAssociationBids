using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ISessionService : IBaseService
    {
        bool Validate(SessionModel item);
        bool IsFilterEnabled(SessionFilterModel filter);
        SessionFilterModel CreateFilter();
        SessionFilterModel CreateFilter(SessionModel item);
        SessionFilterModel UpdateFilter(SessionFilterModel filter);
        SessionModel Create();
        bool Create(SessionModel item);
        bool Update(SessionModel item);
        bool Delete(int id);
        SessionModel Get(int id);
        IList<SessionModel> GetAll();
        IList<SessionModel> GetAll(SessionFilterModel filter);
        IList<SessionModel> GetAll(SessionFilterModel filter, PagingModel paging);
    }
}
