using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPropertyVendorDistanceRepository : IBaseRepository
    {
        bool Create(PropertyVendorDistanceModel item);
        bool Update(PropertyVendorDistanceModel item);
        bool Delete(int id);
        PropertyVendorDistanceModel Get(int id);
        IList<PropertyVendorDistanceModel> GetAll();
        IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter);
        IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter, PagingModel paging);
    }
}
