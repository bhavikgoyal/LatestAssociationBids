using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class ModuleService : AssociationBids.Portal.Service.Site.ModuleService, IModuleService
    {
        new protected IModuleRepository __repository;

        public ModuleService()
            : this(new ModuleRepository()) { }

        public ModuleService(string connectionString)
            : this(new ModuleRepository(connectionString)) { }

        public ModuleService(IModuleRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override ModuleFilterModel UpdateFilter(ModuleFilterModel filter)
        {
            return filter;
        }
    }
}
