using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPortalRepository : IBaseRepository
    {
        bool Create(PortalModel item);
        bool Update(PortalModel item);
        bool Delete(int id);
        PortalModel Get(int id);
        IList<PortalModel> GetAll();
        IList<PortalModel> GetAll(PortalFilterModel filter);
        IList<PortalModel> GetAll(PortalFilterModel filter, PagingModel paging);
    }
}
