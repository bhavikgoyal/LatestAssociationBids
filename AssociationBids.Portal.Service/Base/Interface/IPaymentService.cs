using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IPaymentService : IBaseService
    {
        bool Validate(PaymentModel item);
        bool IsFilterEnabled(PaymentFilterModel filter);
        PaymentFilterModel CreateFilter();
        PaymentFilterModel CreateFilter(PaymentModel item);
        PaymentFilterModel UpdateFilter(PaymentFilterModel filter);
        PaymentModel Create();
        bool Create(PaymentModel item);
        bool Update(PaymentModel item);
        bool Delete(int id);
        PaymentModel Get(int id);
        IList<PaymentModel> GetAll();
        IList<PaymentModel> GetAll(PaymentFilterModel filter);
        IList<PaymentModel> GetAll(PaymentFilterModel filter, PagingModel paging);
    }
}
