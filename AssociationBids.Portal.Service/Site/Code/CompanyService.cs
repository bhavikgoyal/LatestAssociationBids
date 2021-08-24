using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class CompanyService : AssociationBids.Portal.Service.Base.CompanyService, ICompanyService
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
    }
}
