using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ISessionRepository : IBaseRepository
    {
        bool Create(SessionModel item);
        bool Update(SessionModel item);
        bool Delete(int id);
        SessionModel Get(int id);
        IList<SessionModel> GetAll();
        IList<SessionModel> GetAll(SessionFilterModel filter);
        IList<SessionModel> GetAll(SessionFilterModel filter, PagingModel paging);
    }
}
