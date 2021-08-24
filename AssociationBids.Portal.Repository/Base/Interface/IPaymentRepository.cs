using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IPaymentRepository : IBaseRepository
    {
        bool Create(PaymentModel item);
        bool Update(PaymentModel item);
        bool Delete(int id);
        PaymentModel Get(int id);
        IList<PaymentModel> GetAll();
        IList<PaymentModel> GetAll(PaymentFilterModel filter);
        IList<PaymentModel> GetAll(PaymentFilterModel filter, PagingModel paging);
    }
}
