using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class UserAgreementService : AssociationBids.Portal.Service.Site.UserAgreementService, IUserAgreementService
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

        public override UserAgreementFilterModel UpdateFilter(UserAgreementFilterModel filter)
        {
            return filter;
        }
    }
}
