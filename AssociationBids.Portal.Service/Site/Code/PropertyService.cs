using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class PropertyService : AssociationBids.Portal.Service.Base.PropertyService, IPropertyService
    {
        new protected IPropertyRepository __repository;

        public PropertyService()
            : this(new PropertyRepository()) { }

        public PropertyService(string connectionString)
            : this(new PropertyRepository(connectionString)) { }

        public PropertyService(IPropertyRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
