using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Admin
{
    public class TaskRepository : AssociationBids.Portal.Repository.Site.TaskRepository, ITaskRepository
    {
        public TaskRepository() 
            : base() { }

        public TaskRepository(string connectionString)
            : base(connectionString) { }
    }
}
