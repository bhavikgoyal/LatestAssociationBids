using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Repository.Base.Code;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class vBillingService : BaseService, IvBillingService
    {
        protected IvBillingRepository __repository;
        public vBillingService() : this(new vBillingRepository()) { }

        public vBillingService(IvBillingRepository repository)
        {
            ConnectionString = repository.ConnectionString;
            __repository = repository;
        }

        public List<vBillingModel> LoadBillingList(Int64 CompanyKey, Int64 resourceid)
        {
            return __repository.LoadBillingList(CompanyKey, resourceid);
        }
        public List<vBillingModel> LoadBillingListByResurceKey(Int64 resourceid)
        {
            return __repository.LoadBillingListByResurceKey(resourceid);
        }
        public virtual bool Delete(int id)
        {
            return __repository.Remove(id);
        }

        public bool ChangePrimaryMethod(int PaymentMethodKey, int CompanyKey, int ResourceKey)
        {
            return __repository.ChangePrimaryMethod(PaymentMethodKey, CompanyKey, ResourceKey);
        }

        public bool vBillingInsert(vBillingModel vBilling)
        {
            return __repository.vBillingInsert(vBilling);
        }
    }
}
