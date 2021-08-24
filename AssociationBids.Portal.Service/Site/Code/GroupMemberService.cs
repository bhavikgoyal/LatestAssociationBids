using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class GroupMemberService : AssociationBids.Portal.Service.Base.GroupMemberService, IGroupMemberService
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
    }
}
