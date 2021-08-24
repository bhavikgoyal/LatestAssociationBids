using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class BidService : AssociationBids.Portal.Service.Site.BidService, IBidService
    {
        new protected IBidRepository __repository;

        public BidService()
            : this(new BidRepository()) { }

        public BidService(string connectionString)
            : this(new BidRepository(connectionString)) { }

        public BidService(IBidRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override BidFilterModel UpdateFilter(BidFilterModel filter)
        {
            return filter;
        }
    }
}
