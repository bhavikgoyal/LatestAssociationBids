using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class PaymentAppliedService : AssociationBids.Portal.Service.Site.PaymentAppliedService, IPaymentAppliedService
    {
        new protected IPaymentAppliedRepository __repository;

        public PaymentAppliedService()
            : this(new PaymentAppliedRepository()) { }

        public PaymentAppliedService(string connectionString)
            : this(new PaymentAppliedRepository(connectionString)) { }

        public PaymentAppliedService(IPaymentAppliedRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override PaymentAppliedFilterModel UpdateFilter(PaymentAppliedFilterModel filter)
        {
            return filter;
        }
    }
}
