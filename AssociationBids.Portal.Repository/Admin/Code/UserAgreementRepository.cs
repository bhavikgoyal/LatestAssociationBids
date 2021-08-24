using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class UserAgreementRepository : AssociationBids.Portal.Repository.Site.UserAgreementRepository, IUserAgreementRepository
    {
        public UserAgreementRepository() 
            : base() { }

        public UserAgreementRepository(string connectionString)
            : base(connectionString) { }
    }
}
