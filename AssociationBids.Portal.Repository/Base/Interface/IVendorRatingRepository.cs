using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IVendorRatingRepository : IBaseRepository
    {
        bool Create(VendorRatingModel item);
        bool Update(VendorRatingModel item);
        bool Delete(int id);
        VendorRatingModel Get(int id);
        IList<VendorRatingModel> GetAll();
        IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter);
        IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter, PagingModel paging);
    }
}
