using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class ModuleRepository : AssociationBids.Portal.Repository.Base.ModuleRepository, IModuleRepository
    {
        public ModuleRepository() 
            : base() { }

        public ModuleRepository(string connectionString)
            : base(connectionString) { }
    }
}
