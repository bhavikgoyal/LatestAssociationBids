using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class TaskService : AssociationBids.Portal.Service.Base.TaskService, ITaskService
    {
        new protected ITaskRepository __repository;

        public TaskService()
            : this(new TaskRepository()) { }

        public TaskService(string connectionString)
            : this(new TaskRepository(connectionString)) { }

        public TaskService(ITaskRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
