using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class CompanyVendorService : AssociationBids.Portal.Service.Base.CompanyVendorService, ICompanyVendorService
    {
        new protected ICompanyVendorRepository __repository;

        public CompanyVendorService()
            : this(new CompanyVendorRepository()) { }

        public CompanyVendorService(string connectionString)
            : this(new CompanyVendorRepository(connectionString)) { }

        public CompanyVendorService(ICompanyVendorRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
