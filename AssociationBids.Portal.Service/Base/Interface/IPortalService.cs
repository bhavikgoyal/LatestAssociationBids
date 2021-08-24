using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPortalService : IBaseService
    {
        bool Validate(PortalModel item);
        bool IsFilterEnabled(PortalFilterModel filter);
        PortalFilterModel CreateFilter();
        PortalFilterModel CreateFilter(PortalModel item);
        PortalFilterModel UpdateFilter(PortalFilterModel filter);
        PortalModel Create();
        bool Create(PortalModel item);
        bool Update(PortalModel item);
        bool Delete(int id);
        PortalModel Get(int id);
        IList<PortalModel> GetAll();
        IList<PortalModel> GetAll(PortalFilterModel filter);
        IList<PortalModel> GetAll(PortalFilterModel filter, PagingModel paging);
    }
}
