using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IRegistrationRepository: IBaseRepository
    {

        IList<RegistrationModel> GetAllState();
        IList<RegistrationModel> GetAllService();
        Int64 Insert(RegistrationModel registrationModel);
        bool PaymentInsert(RegistrationModel registrationModel, string CardNumber, int Month, int Year, string CVV, string Firstname, string lastname);

        bool PaymentInsert_New(string TokenId, string CVV);

        int IsCompanyName(int Id, string name);
        int IsEmailExist(string name,int ResourceKey);
        RegistrationModel Getvendordetails(int companykey);
        RegistrationModel GeAgreementDetails(int companykey);
        bool GetLinkExpiredCheck(int companykey);

    }
}
