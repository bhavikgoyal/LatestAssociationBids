using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class GroupMemberRepository : AssociationBids.Portal.Repository.Site.GroupMemberRepository, IGroupMemberRepository
    {
        public GroupMemberRepository() 
            : base() { }

        public GroupMemberRepository(string connectionString)
            : base(connectionString) { }
    }
}
