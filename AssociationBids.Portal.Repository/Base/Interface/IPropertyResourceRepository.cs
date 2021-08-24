using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPropertyResourceRepository : IBaseRepository
    {
        bool Create(PropertyResourceModel item);
        bool Update(PropertyResourceModel item);
        bool Delete(int id);
        PropertyResourceModel Get(int id);
        IList<PropertyResourceModel> GetAll();
        IList<PropertyResourceModel> GetAll(PropertyResourceFilterModel filter);
        IList<PropertyResourceModel> GetAll(PropertyResourceFilterModel filter, PagingModel paging);
    }
}
