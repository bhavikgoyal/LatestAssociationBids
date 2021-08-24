using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class PaymentRepository : AssociationBids.Portal.Repository.Base.PaymentRepository, IPaymentRepository
    {
        public PaymentRepository() 
            : base() { }

        public PaymentRepository(string connectionString)
            : base(connectionString) { }
    }
}
