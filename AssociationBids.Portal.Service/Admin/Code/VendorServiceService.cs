using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Admin
{
    public class VendorServiceService : AssociationBids.Portal.Service.Site.VendorServiceService, IVendorServiceService
    {
        new protected IVendorServiceRepository __repository;

        public VendorServiceService()
            : this(new VendorServiceRepository()) { }

        public VendorServiceService(string connectionString)
            : this(new VendorServiceRepository(connectionString)) { }

        public VendorServiceService(IVendorServiceRepository repository)
            
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public override VendorServiceFilterModel UpdateFilter(VendorServiceFilterModel filter)
        {
            return filter;
        }
    }
}
