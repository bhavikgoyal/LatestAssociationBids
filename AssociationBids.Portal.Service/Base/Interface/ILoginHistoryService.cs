using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ILoginHistoryService : IBaseService
    {
        bool Validate(LoginHistoryModel item);
        bool IsFilterEnabled(LoginHistoryFilterModel filter);
        LoginHistoryFilterModel CreateFilter();
        LoginHistoryFilterModel CreateFilter(LoginHistoryModel item);
        LoginHistoryFilterModel UpdateFilter(LoginHistoryFilterModel filter);
        LoginHistoryModel Create();
        bool Create(LoginHistoryModel item);
        bool Update(LoginHistoryModel item);
        bool Delete(int id);
        LoginHistoryModel Get(int id);
        IList<LoginHistoryModel> GetAll();
        IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter);
        IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter, PagingModel paging);
    }
}
