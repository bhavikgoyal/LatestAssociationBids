using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPropertyVendorDistanceService : IBaseService
    {
        bool Validate(PropertyVendorDistanceModel item);
        bool IsFilterEnabled(PropertyVendorDistanceFilterModel filter);
        PropertyVendorDistanceFilterModel CreateFilter();
        PropertyVendorDistanceFilterModel CreateFilter(PropertyVendorDistanceModel item);
        PropertyVendorDistanceFilterModel UpdateFilter(PropertyVendorDistanceFilterModel filter);
        PropertyVendorDistanceModel Create();
        bool Create(PropertyVendorDistanceModel item);
        bool Update(PropertyVendorDistanceModel item);
        bool Delete(int id);
        PropertyVendorDistanceModel Get(int id);
        IList<PropertyVendorDistanceModel> GetAll();
        IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter);
        IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter, PagingModel paging);
    }
}
