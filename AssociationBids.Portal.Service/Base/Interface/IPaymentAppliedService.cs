using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPaymentAppliedService : IBaseService
    {
        bool Validate(PaymentAppliedModel item);
        bool IsFilterEnabled(PaymentAppliedFilterModel filter);
        PaymentAppliedFilterModel CreateFilter();
        PaymentAppliedFilterModel CreateFilter(PaymentAppliedModel item);
        PaymentAppliedFilterModel UpdateFilter(PaymentAppliedFilterModel filter);
        PaymentAppliedModel Create();
        bool Create(PaymentAppliedModel item);
        bool Update(PaymentAppliedModel item);
        bool Delete(int id);
        PaymentAppliedModel Get(int id);
        IList<PaymentAppliedModel> GetAll();
        IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter);
        IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter, PagingModel paging);
    }
}
