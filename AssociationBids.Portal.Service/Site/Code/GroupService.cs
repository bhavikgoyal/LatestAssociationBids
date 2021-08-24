using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class GroupService : AssociationBids.Portal.Service.Base.GroupService, IGroupService
    {
        new protected IGroupRepository __repository;

        public GroupService()
            : this(new GroupRepository()) { }

        public GroupService(string connectionString)
            : this(new GroupRepository(connectionString)) { }

        public GroupService(IGroupRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
