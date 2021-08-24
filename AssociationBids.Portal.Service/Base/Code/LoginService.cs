using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model.Login;
using AssociationBids.Portal.Repository.Login;

namespace AssociationBids.Portal.Service.Login
{
    public class LoginService : ILoginService
    {
        public readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public LoginResponseModel GetUsersDetails(LoginModel loginModel)
        {
            return (_loginRepository.GetUsersDetails(loginModel));
        }
       
    }
}
