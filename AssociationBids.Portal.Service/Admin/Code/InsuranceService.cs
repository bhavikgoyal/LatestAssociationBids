using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class InsuranceService : AssociationBids.Portal.Service.Site.InsuranceService, IInsuranceService
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

        public override InsuranceFilterModel UpdateFilter(InsuranceFilterModel filter)
        {
            return filter;
        }
    }
}
