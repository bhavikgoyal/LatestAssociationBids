using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IvBillingRepository : IBaseRepository
    {
        List<vBillingModel>  LoadBillingList(Int64 CompanyKey, Int64 resourceid);
        List<vBillingModel> LoadBillingListByResurceKey(Int64 resourceid);
        bool Remove(int id);
        bool ChangePrimaryMethod(int PaymentMethodKey, int CompanyKey, int ResourceKey);
        bool vBillingInsert(vBillingModel vBilling);
    }
}
