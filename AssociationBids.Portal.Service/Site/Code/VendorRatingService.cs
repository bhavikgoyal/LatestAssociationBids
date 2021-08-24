using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class VendorRatingService : AssociationBids.Portal.Service.Base.VendorRatingService, IVendorRatingService
    {
        new protected IVendorRatingRepository __repository;

        public VendorRatingService()
            : this(new VendorRatingRepository()) { }

        public VendorRatingService(string connectionString)
            : this(new VendorRatingRepository(connectionString)) { }

        public VendorRatingService(IVendorRatingRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
