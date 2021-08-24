using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IChangePasswordRepository : IBaseRepository
    {
        int ChangePassword(ChangePasswordModel changePasswordModel);
        List<ChangePasswordModel> CheckToken(int ResourceKey);
        int ResetPassword(ChangePasswordModel changePasswordModel);
        int ResetPasswordByUser(ChangePasswordModel changePasswordModel);
        ChangePasswordModel GeAgreementDetails();
        bool UpdateTermsConditions(string Terms, int UserKey, int Aggrementkey);
    }
}
