using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class PaymentAppliedRepository : AssociationBids.Portal.Repository.Site.PaymentAppliedRepository, IPaymentAppliedRepository
    {
        public PaymentAppliedRepository() 
            : base() { }

        public PaymentAppliedRepository(string connectionString)
            : base(connectionString) { }
    }
}
