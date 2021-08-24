using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class LookUpTypeService : AssociationBids.Portal.Service.Site.LookUpTypeService, ILookUpTypeService
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

        public override LookUpTypeFilterModel UpdateFilter(LookUpTypeFilterModel filter)
        {
            return filter;
        }
    }
}
