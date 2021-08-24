using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class BidRequestService : AssociationBids.Portal.Service.Base.BidRequestService, IBidRequestService
    {
        new protected IBidRequestRepository __repository;

        public BidRequestService()
            : this(new BidRequestRepository()) { }

        public BidRequestService(string connectionString)
            : this(new BidRequestRepository(connectionString)) { }

        public BidRequestService(IBidRequestRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
