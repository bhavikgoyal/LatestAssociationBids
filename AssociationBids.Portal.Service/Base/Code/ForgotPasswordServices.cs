using AssociationBids.Portal.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base
{
    public class ForgotPasswordServices : BaseService, IForgotPasswordServices
    {
        protected IForgotPasswordRepository __repository;

        public ForgotPasswordServices() : this(new ForgotPasswordRepository()) { }

        public ForgotPasswordServices(IForgotPasswordRepository ForgotPassword)
        {
            ConnectionString = ForgotPassword.ConnectionString;

            __repository = ForgotPassword;
        }

        public bool CheckEmail(string Email)
        {
            return __repository.CheckEmail(Email);
        }
    }
}
