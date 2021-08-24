using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IBidRepository : IBaseRepository
    {
        bool Create(BidModel item);
        bool Update(BidModel item);
        bool Delete(int id);
        BidModel Get(int id);
        IList<BidModel> GetAll();
        IList<BidModel> GetAll(BidFilterModel filter);
        IList<BidModel> GetAll(BidFilterModel filter, PagingModel paging);
    }
}
