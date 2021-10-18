using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System.Collections.Generic;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class RegistrationService : BaseService, IRegistrationService
    {
        protected IRegistrationRepository __repository;
        public RegistrationService() : this(new RegistrationRepository()) { }
        public RegistrationService(IRegistrationRepository repository)
        {
            ConnectionString = repository.ConnectionString;
            __repository = repository;
        }

      

        public IList<RegistrationModel> GetAllService()
        {
            return __repository.GetAllService();
          
        }

        public IList<RegistrationModel> GetAllState()
        {
            return __repository.GetAllState();

        }

        public long Insert(RegistrationModel registrationModel)
        {
            return __repository.Insert(registrationModel);
        }

        public bool PaymentInsert(RegistrationModel registrationModel, string CardNumber, int Month, int Year, string CVV, string Firstname, string lastname)
        {
            return __repository.PaymentInsert(registrationModel, CardNumber, Month, Year, CVV,  Firstname,  lastname);
        }

        public bool PaymentInsert_New(string TokenId, string CVV)
        {
            return __repository.PaymentInsert_New(TokenId, CVV);
        }


        public int IsEmailExist(string name,int ResourceKey)
        {
            return __repository.IsEmailExist(name,ResourceKey);
        }

        public int IsCompanyName(int Id, string name)
        {
            return __repository.IsCompanyName(Id,name);
        }
        public RegistrationModel Getvendordetails(int companykey)
        {
            return __repository.Getvendordetails(companykey);
        }

        public RegistrationModel GeAgreementDetails(int companykey)
        {
            return __repository.GeAgreementDetails(companykey);
        }
        public bool GetLinkExpiredCheck(int companykey)
        {
            return __repository.GetLinkExpiredCheck(companykey);
        }
    }
}
