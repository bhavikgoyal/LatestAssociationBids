using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IBidVendorRepository : IBaseRepository
    {
        bool Create(BidVendorModel item);
        bool Update(BidVendorModel item);
        bool Delete(int id);
        BidVendorModel Get(int id);
        IList<BidVendorModel> GetAll();
        IList<BidVendorModel> GetAll(BidVendorFilterModel filter);
        IList<BidVendorModel> GetAll(BidVendorFilterModel filter, PagingModel paging);
    }
}
