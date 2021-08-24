using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class UserAgreementRepository : AssociationBids.Portal.Repository.Base.UserAgreementRepository, IUserAgreementRepository
    {
        public UserAgreementRepository() 
            : base() { }

        public UserAgreementRepository(string connectionString)
            : base(connectionString) { }
    }
}
