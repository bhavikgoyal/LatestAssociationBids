using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IForgotPasswordRepository : IBaseRepository
    {
        bool CheckEmail(string Email);
    }
}
