using AssociationBids.Portal.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Login
{
    public interface ILoginService
    {
        LoginResponseModel GetUsersDetails(LoginModel loginModel);
    }
}
