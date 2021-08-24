using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class GroupMemberService : AssociationBids.Portal.Service.Site.GroupMemberService, IGroupMemberService
    {
        new protected IGroupMemberRepository __repository;

        public GroupMemberService()
            : this(new GroupMemberRepository()) { }

        public GroupMemberService(string connectionString)
            : this(new GroupMemberRepository(connectionString)) { }

        public GroupMemberService(IGroupMemberRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override GroupMemberFilterModel UpdateFilter(GroupMemberFilterModel filter)
        {
            return filter;
        }
    }
}
