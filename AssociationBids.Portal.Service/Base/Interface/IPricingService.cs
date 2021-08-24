using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPricingService : IBaseService
    {
        bool Validate(PricingModel item);
        bool IsFilterEnabled(PricingFilterModel filter);
        PricingFilterModel CreateFilter();
        PricingFilterModel CreateFilter(PricingModel item);
        PricingFilterModel UpdateFilter(PricingFilterModel filter);
        PricingModel Create();
        bool Create(PricingModel item);
        bool Update(PricingModel item);
        bool Delete(int id);
        PricingModel Get(int id);
        IList<PricingModel> GetAll();
        IList<PricingModel> GetAll(PricingFilterModel filter);
        IList<PricingModel> GetAll(PricingFilterModel filter, PagingModel paging);
    }
}
