using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IVendorServiceRepository : IBaseRepository
    {
        bool Create(VendorServiceModel item);
        bool Update(VendorServiceModel item);
        bool Delete(int id);
        VendorServiceModel Get(int id);
        IList<VendorServiceModel> GetAll();
        IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter);
        IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter, PagingModel paging);
    }
}
