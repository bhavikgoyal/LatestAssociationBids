using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IVendorServiceService : IBaseService
    {
        bool Validate(VendorServiceModel item);
        bool IsFilterEnabled(VendorServiceFilterModel filter);
        VendorServiceFilterModel CreateFilter();
        VendorServiceFilterModel CreateFilter(VendorServiceModel item);
        VendorServiceFilterModel UpdateFilter(VendorServiceFilterModel filter);
        VendorServiceModel Create();
        bool Create(VendorServiceModel item);
        bool Update(VendorServiceModel item);
        bool Delete(int id);
        VendorServiceModel Get(int id);
        IList<VendorServiceModel> GetAll();
        IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter);
        IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter, PagingModel paging);
    }
}
