using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class CompanyService : AssociationBids.Portal.Service.Site.CompanyService, ICompanyService
    {
        new protected ICompanyRepository __repository;

        public CompanyService()
            : this(new CompanyRepository()) { }

        public CompanyService(string connectionString)
            : this(new CompanyRepository(connectionString)) { }

        public CompanyService(ICompanyRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override CompanyFilterModel UpdateFilter(CompanyFilterModel filter)
        {
            return base.UpdateFilter<CompanyFilterModel>(filter);
        }
    }
}
