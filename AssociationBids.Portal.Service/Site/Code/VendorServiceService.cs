using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class VendorServiceService : AssociationBids.Portal.Service.Base.VendorServiceService, IVendorServiceService
    {
        new protected IVendorServiceRepository __repository;

        public VendorServiceService()
            : this(new VendorServiceRepository()) { }

        public VendorServiceService(string connectionString)
            : this(new VendorServiceRepository(connectionString)) { }

        public VendorServiceService(IVendorServiceRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
