using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

//namespace AssociationBids.Portal.Service.Base.Interface
namespace AssociationBids.Portal.Service.Base
{
    public interface INewPricingService : IBaseService
    {
        IList<LookUpModel> GetAllTitle();
        IList<LookUpModel> GetAllLookUpTitle(String Type);
        Int64 Insert(PricingModel item);
        List<PricingModel> SearchPricing(Int64 PageSize, Int64 PageIndex, string Search, string Sort);
        PricingModel GetDataViewEdit(int PricingKey);
        Int64  PricingEdit(PricingModel item);
       
        bool Delete(int id);
        
    }


    //class INewPricingService
    //{

    //}
}
