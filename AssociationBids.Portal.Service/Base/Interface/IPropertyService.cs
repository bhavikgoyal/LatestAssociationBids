using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPropertyService : IBaseService
    {
        bool Validate(PropertyModel item);
        bool IsFilterEnabled(PropertyFilterModel filter);
        PropertyFilterModel CreateFilter();
        PropertyFilterModel CreateFilter(PropertyModel item);
        PropertyFilterModel UpdateFilter(PropertyFilterModel filter);
        PropertyModel Create();
        bool Create(PropertyModel item);
        bool Update(PropertyModel item);
        bool Delete(int id);
        PropertyModel Get(int id);
        IList<PropertyModel> GetAll();
        IList<PropertyModel> GetAll(PropertyFilterModel filter);
        IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging);
    }
}
