using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class GroupService : AssociationBids.Portal.Service.Site.GroupService, IGroupService
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

        public override GroupFilterModel UpdateFilter(GroupFilterModel filter)
        {
            return filter;
        }
    }
}
