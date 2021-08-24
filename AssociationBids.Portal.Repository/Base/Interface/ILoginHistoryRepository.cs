using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ILoginHistoryRepository : IBaseRepository
    {
        bool Create(LoginHistoryModel item);
        bool Update(LoginHistoryModel item);
        bool Delete(int id);
        LoginHistoryModel Get(int id);
        IList<LoginHistoryModel> GetAll();
        IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter);
        IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter, PagingModel paging);
    }
}
