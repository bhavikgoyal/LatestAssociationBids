using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class NewPricingService : BaseService, INewPricingService
    {
        protected INewPricingRepository __repository;
        public NewPricingService() : this(new NewPricingRepository()) { }
       
        public NewPricingService(INewPricingRepository repository)
        {
            ConnectionString = repository.ConnectionString;
            __repository = repository;
        }

        public IList<LookUpModel> GetAllTitle()
        {
            return __repository.GetAllLookUp();
        }
        public IList<LookUpModel> GetAllLookUpTitle(String Type)
        {
            return __repository.GetAllLookUpTitle(Type);
        }

        public Int64 Insert(PricingModel item)
        {
            return __repository.Insert(item);
        }

        public List<PricingModel> SearchPricing(Int64 PageSize, Int64 PageIndex, string Search, string Sort)
        {
            return __repository.SearchPricing(PageSize, PageIndex, Search, Sort);
        }

        public virtual PricingModel GetDataViewEdit(int PricingKey)
        {
            return __repository.GetDataViewEdit(PricingKey);
        }

        public Int64 PricingEdit(PricingModel item)
        {
            return __repository.PricingEdit(item);
        }

       

        public virtual bool Delete(int id)
        {
            return __repository.Remove(id);
        }

    }
}
