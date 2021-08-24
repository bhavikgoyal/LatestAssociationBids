using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base
{
    public interface IChangePasswordService : IBaseService
    {
        Int32 ChangePassword(ChangePasswordModel changePasword);
        List<ChangePasswordModel> CheckToken(int ResourceKey);
        Int32 ResetPassword(ChangePasswordModel changePasword);
        Int32 ResetPasswordByUser(ChangePasswordModel changePasword);
        ChangePasswordModel GeAgreementDetails();
       bool UpdateTermsConditions(string Terms, int UserKey, int Agreementkey);
    }
}
