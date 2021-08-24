using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPaymentAppliedRepository : IBaseRepository
    {
        bool Create(PaymentAppliedModel item);
        bool Update(PaymentAppliedModel item);
        bool Delete(int id);
        PaymentAppliedModel Get(int id);
        IList<PaymentAppliedModel> GetAll();
        IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter);
        IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter, PagingModel paging);
    }
}
