using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPropertyResourceService : IBaseService
    {
        bool Validate(PropertyResourceModel item);
        bool IsFilterEnabled(PropertyResourceFilterModel filter);
        PropertyResourceFilterModel CreateFilter();
        PropertyResourceFilterModel CreateFilter(PropertyResourceModel item);
        PropertyResourceFilterModel UpdateFilter(PropertyResourceFilterModel filter);
        PropertyResourceModel Create();
        bool Create(PropertyResourceModel item);
        bool Update(PropertyResourceModel item);
        bool Delete(int id);
        PropertyResourceModel Get(int id);
        IList<PropertyResourceModel> GetAll();
        IList<PropertyResourceModel> GetAll(PropertyResourceFilterModel filter);
        IList<PropertyResourceModel> GetAll(PropertyResourceFilterModel filter, PagingModel paging);
    }
}
