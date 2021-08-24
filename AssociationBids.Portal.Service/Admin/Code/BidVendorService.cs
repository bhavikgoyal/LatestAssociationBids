using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class BidVendorService : AssociationBids.Portal.Service.Site.BidVendorService, IBidVendorService
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

        public override BidVendorFilterModel UpdateFilter(BidVendorFilterModel filter)
        {
            return filter;
        }
    }
}
