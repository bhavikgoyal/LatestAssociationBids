using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IBidService : IBaseService
    {
        bool Validate(BidModel item);
        bool IsFilterEnabled(BidFilterModel filter);
        BidFilterModel CreateFilter();
        BidFilterModel CreateFilter(BidModel item);
        BidFilterModel UpdateFilter(BidFilterModel filter);
        BidModel Create();
        bool Create(BidModel item);
        bool Update(BidModel item);
        bool Delete(int id);
        BidModel Get(int id);
        IList<BidModel> GetAll();
        IList<BidModel> GetAll(BidFilterModel filter);
        IList<BidModel> GetAll(BidFilterModel filter, PagingModel paging);
    }
}
