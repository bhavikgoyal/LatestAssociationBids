using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

//namespace AssociationBids.Portal.Repository.Base.Interface
namespace AssociationBids.Portal.Repository.Base
{
    public interface INewPricingRepository : IBaseRepository
    {
        IList<LookUpModel> GetAllLookUp();
        IList<LookUpModel> GetAllLookUpTitle(String Type);
        Int64 Insert(PricingModel item);
        List<PricingModel> SearchPricing(Int64 PageSize, Int64 PageIndex, string Search, string Sort);
        PricingModel GetDataViewEdit(int PricingKey);
        Int64 PricingEdit(PricingModel item);
        bool Remove(int id);

    }
    //class INewPricingRepository 
    //{
    //}
}
