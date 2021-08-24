using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPromotionService : IBaseService
    {
        bool Validate(PromotionModel item);
        bool IsFilterEnabled(PromotionFilterModel filter);
        PromotionFilterModel CreateFilter();
        PromotionFilterModel CreateFilter(PromotionModel item);
        PromotionFilterModel UpdateFilter(PromotionFilterModel filter);
        PromotionModel Create();
        bool Create(PromotionModel item);
        bool Update(PromotionModel item);
        bool Delete(int id);
        PromotionModel Get(int id);
        IList<PromotionModel> GetAll();
        IList<PromotionModel> GetAll(PromotionFilterModel filter);
        IList<PromotionModel> GetAll(PromotionFilterModel filter, PagingModel paging);
    }
}
