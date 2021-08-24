using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IServiceAreaRepository : IBaseRepository
    {
        bool Create(ServiceAreaModel item);
        bool Update(ServiceAreaModel item);
        bool Delete(int id);
        ServiceAreaModel Get(int id);
        IList<ServiceAreaModel> GetAll();
        IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter);
        IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter, PagingModel paging);
    }
}
