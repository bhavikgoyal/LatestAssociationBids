using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class PaymentService : AssociationBids.Portal.Service.Base.PaymentService, IPaymentService
    {
        new protected IPaymentRepository __repository;

        public PaymentService()
            : this(new PaymentRepository()) { }

        public PaymentService(string connectionString)
            : this(new PaymentRepository(connectionString)) { }

        public PaymentService(IPaymentRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
