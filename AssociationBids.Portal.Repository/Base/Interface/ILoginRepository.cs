using AssociationBids.Portal.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Login
{
    public interface ILoginRepository
    {
        LoginResponseModel GetUsersDetails(LoginModel loginModel);

        
    }
}
