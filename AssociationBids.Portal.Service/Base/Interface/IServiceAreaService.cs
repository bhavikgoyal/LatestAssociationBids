using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IServiceAreaService : IBaseService
    {
        bool Validate(ServiceAreaModel item);
        bool IsFilterEnabled(ServiceAreaFilterModel filter);
        ServiceAreaFilterModel CreateFilter();
        ServiceAreaFilterModel CreateFilter(ServiceAreaModel item);
        ServiceAreaFilterModel UpdateFilter(ServiceAreaFilterModel filter);
        ServiceAreaModel Create();
        bool Create(ServiceAreaModel item);
        bool Update(ServiceAreaModel item);
        bool Delete(int id);
        ServiceAreaModel Get(int id);
        IList<ServiceAreaModel> GetAll();
        IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter);
        IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter, PagingModel paging);
    }
}
