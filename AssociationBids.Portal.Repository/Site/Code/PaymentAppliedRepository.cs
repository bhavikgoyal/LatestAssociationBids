using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class PaymentAppliedRepository : AssociationBids.Portal.Repository.Base.PaymentAppliedRepository, IPaymentAppliedRepository
    {
        public PaymentAppliedRepository() 
            : base() { }

        public PaymentAppliedRepository(string connectionString)
            : base(connectionString) { }
    }
}
