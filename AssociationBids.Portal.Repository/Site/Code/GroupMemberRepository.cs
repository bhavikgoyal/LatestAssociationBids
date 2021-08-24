using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class GroupMemberRepository : AssociationBids.Portal.Repository.Base.GroupMemberRepository, IGroupMemberRepository
    {
        public GroupMemberRepository() 
            : base() { }

        public GroupMemberRepository(string connectionString)
            : base(connectionString) { }
    }
}
