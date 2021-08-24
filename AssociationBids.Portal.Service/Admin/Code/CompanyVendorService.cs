using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class CompanyVendorService : AssociationBids.Portal.Service.Site.CompanyVendorService, ICompanyVendorService
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

        public override CompanyVendorFilterModel UpdateFilter(CompanyVendorFilterModel filter)
        {
            return filter;
        }
    }
}
