using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class BidService : AssociationBids.Portal.Service.Base.BidService, IBidService
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
    }
}
