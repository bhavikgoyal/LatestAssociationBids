using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class AgreementService : AssociationBids.Portal.Service.Site.AgreementService, IAgreementService
    {
        new protected IAgreementRepository __repository;

        //public AgreementService()
        //    : this(new AgreementRepository()) { }

        //public AgreementService(string connectionString)
        //    : this(new AgreementRepository(connectionString)) { }

        //public AgreementService(IAgreementRepository repository)
        //    : base(repository)
        //{
        //    __repository = repository;
        //}

        public override AgreementFilterModel UpdateFilter(AgreementFilterModel filter)
        {
            return base.UpdateFilter<AgreementFilterModel>(filter);
        }
    }
}
