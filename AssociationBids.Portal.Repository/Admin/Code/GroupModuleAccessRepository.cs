using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class GroupModuleAccessRepository : AssociationBids.Portal.Repository.Site.GroupModuleAccessRepository, IGroupModuleAccessRepository
    {
        public GroupModuleAccessRepository() 
            : base() { }

        public GroupModuleAccessRepository(string connectionString)
            : base(connectionString) { }
    }
}
