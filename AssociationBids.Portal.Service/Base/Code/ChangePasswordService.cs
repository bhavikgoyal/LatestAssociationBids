using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.ChangePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class ChangePasswordService : BaseService, IChangePasswordService
    {
        protected IChangePasswordRepository __repository;

        public ChangePasswordService() : this(new ChangePasswordRepository()) { }

        public ChangePasswordService(string connectionString) : this(new ChangePasswordRepository(connectionString)) { }
        
        public readonly IChangePasswordRepository _changePasswordRepository;

        public ChangePasswordService(IChangePasswordRepository changePasswordRepository)
        {
            _changePasswordRepository = changePasswordRepository;
        }

        public int ChangePassword(ChangePasswordModel changePasswordModel)
        {
            return (_changePasswordRepository.ChangePassword(changePasswordModel));
        }

        public List<ChangePasswordModel> CheckToken(int ResourceKey)
        {
            return (_changePasswordRepository.CheckToken(ResourceKey));
        }


        public ChangePasswordModel GeAgreementDetails()
        {
            return (_changePasswordRepository.GeAgreementDetails());
        }

        public bool UpdateTermsConditions(string Terms, int UserKey, int  AggrementKey)
        {
            return (_changePasswordRepository.UpdateTermsConditions(Terms, UserKey, AggrementKey));
        }

        public int ResetPassword(ChangePasswordModel changePasswordModel)
        {
            return (_changePasswordRepository.ResetPassword(changePasswordModel));
        }

        public int ResetPasswordByUser(ChangePasswordModel changePasswordModel)
        {
            return (_changePasswordRepository.ResetPasswordByUser(changePasswordModel));
        }
    }
}
