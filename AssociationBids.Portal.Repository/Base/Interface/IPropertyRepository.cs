using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPropertyRepository : IBaseRepository
    {
        bool Create(PropertyModel item);
        bool Update(PropertyModel item);
        bool Delete(int id);
        PropertyModel Get(int id);
        IList<PropertyModel> GetAll();
        IList<PropertyModel> GetAll(PropertyFilterModel filter);
        IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging);
    }
}
