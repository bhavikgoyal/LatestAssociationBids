using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IVendorRatingService : IBaseService
    {
        bool Validate(VendorRatingModel item);
        bool IsFilterEnabled(VendorRatingFilterModel filter);
        VendorRatingFilterModel CreateFilter();
        VendorRatingFilterModel CreateFilter(VendorRatingModel item);
        VendorRatingFilterModel UpdateFilter(VendorRatingFilterModel filter);
        VendorRatingModel Create();
        bool Create(VendorRatingModel item);
        bool Update(VendorRatingModel item);
        bool Delete(int id);
        VendorRatingModel Get(int id);
        IList<VendorRatingModel> GetAll();
        IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter);
        IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter, PagingModel paging);
    }
}
