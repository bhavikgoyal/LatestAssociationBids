using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class LookUpTypeService : AssociationBids.Portal.Service.Base.LookUpTypeService, ILookUpTypeService
    {
        new protected ILookUpTypeRepository __repository;

        public LookUpTypeService()
            : this(new LookUpTypeRepository()) { }

        public LookUpTypeService(string connectionString)
            : this(new LookUpTypeRepository(connectionString)) { }

        public LookUpTypeService(ILookUpTypeRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
