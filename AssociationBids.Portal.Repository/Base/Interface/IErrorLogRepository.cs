using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IErrorLogRepository : IBaseRepository
    {
        bool Create(ErrorLogModel item);
        bool Update(ErrorLogModel item);
        bool Delete(int id);
        ErrorLogModel Get(int id);
        IList<ErrorLogModel> GetAll();
        IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter);
        IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter, PagingModel paging);
    }
}
