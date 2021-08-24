using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class BidVendorService : AssociationBids.Portal.Service.Base.BidVendorService, IBidVendorService
    {
        new protected IBidVendorRepository __repository;

        public BidVendorService()
            : this(new BidVendorRepository()) { }

        public BidVendorService(string connectionString)
            : this(new BidVendorRepository(connectionString)) { }

        public BidVendorService(IBidVendorRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
