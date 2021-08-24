using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IBidVendorService : IBaseService
    {
        bool Validate(BidVendorModel item);
        bool IsFilterEnabled(BidVendorFilterModel filter);
        BidVendorFilterModel CreateFilter();
        BidVendorFilterModel CreateFilter(BidVendorModel item);
        BidVendorFilterModel UpdateFilter(BidVendorFilterModel filter);
        BidVendorModel Create();
        bool Create(BidVendorModel item);
        bool Update(BidVendorModel item);
        bool Delete(int id);
        BidVendorModel Get(int id);
        IList<BidVendorModel> GetAll();
        IList<BidVendorModel> GetAll(BidVendorFilterModel filter);
        IList<BidVendorModel> GetAll(BidVendorFilterModel filter, PagingModel paging);
    }
}
