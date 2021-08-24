using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPricingRepository : IBaseRepository
    {
        bool Create(PricingModel item);
        bool Update(PricingModel item);
        bool Delete(int id);
        PricingModel Get(int id);
        IList<PricingModel> GetAll();
        IList<PricingModel> GetAll(PricingFilterModel filter);
        IList<PricingModel> GetAll(PricingFilterModel filter, PagingModel paging);
    }
}
