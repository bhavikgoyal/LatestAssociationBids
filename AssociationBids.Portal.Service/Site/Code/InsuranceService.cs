using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class InsuranceService : AssociationBids.Portal.Service.Base.InsuranceService, IInsuranceService
    {
        new protected IInsuranceRepository __repository;

        public InsuranceService()
            : this(new InsuranceRepository()) { }

        public InsuranceService(string connectionString)
            : this(new InsuranceRepository(connectionString)) { }

        public InsuranceService(IInsuranceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
