using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPromotionRepository : IBaseRepository
    {
        bool Create(PromotionModel item);
        bool Update(PromotionModel item);
        bool Delete(int id);
        PromotionModel Get(int id);
        IList<PromotionModel> GetAll();
        IList<PromotionModel> GetAll(PromotionFilterModel filter);
        IList<PromotionModel> GetAll(PromotionFilterModel filter, PagingModel paging);
    }
}
