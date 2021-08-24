using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class GroupModuleAccessService : AssociationBids.Portal.Service.Base.GroupModuleAccessService, IGroupModuleAccessService
    {


        new protected IGroupModuleAccessRepository __repository;

        public GroupModuleAccessService()
            : this(new GroupModuleAccessRepository()) { }

        public GroupModuleAccessService(string connectionString)
            : this(new GroupModuleAccessRepository(connectionString)) { }

        public GroupModuleAccessService(IGroupModuleAccessRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
