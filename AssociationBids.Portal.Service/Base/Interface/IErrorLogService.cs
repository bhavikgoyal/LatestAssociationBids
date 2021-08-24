using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IErrorLogService : IBaseService
    {
        bool Validate(ErrorLogModel item);
        bool IsFilterEnabled(ErrorLogFilterModel filter);
        ErrorLogFilterModel CreateFilter();
        ErrorLogFilterModel CreateFilter(ErrorLogModel item);
        ErrorLogFilterModel UpdateFilter(ErrorLogFilterModel filter);
        ErrorLogModel Create();
        bool Create(ErrorLogModel item);
        bool Update(ErrorLogModel item);
        bool Delete(int id);
        ErrorLogModel Get(int id);
        IList<ErrorLogModel> GetAll();
        IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter);
        IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter, PagingModel paging);
    }
}
