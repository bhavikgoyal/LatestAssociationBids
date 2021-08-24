using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base
{
    public interface IForgotPasswordServices : IBaseService
    {
        bool CheckEmail(string Email);
    }
}
