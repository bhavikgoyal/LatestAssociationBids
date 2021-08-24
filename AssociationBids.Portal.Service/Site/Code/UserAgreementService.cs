using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class UserAgreementService : AssociationBids.Portal.Service.Base.UserAgreementService, IUserAgreementService
    {
        new protected IUserAgreementRepository __repository;

        public UserAgreementService()
            : this(new UserAgreementRepository()) { }

        public UserAgreementService(string connectionString)
            : this(new UserAgreementRepository(connectionString)) { }

        public UserAgreementService(IUserAgreementRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
