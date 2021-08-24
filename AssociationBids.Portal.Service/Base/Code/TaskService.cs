using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class TaskService : BaseService, ITaskService
    {
        protected ITaskRepository __repository;

        public TaskService()
            : this(new TaskRepository()) { }

        public TaskService(string connectionString)
            : this(new TaskRepository(connectionString)) { }

        public TaskService(ITaskRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(TaskModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidText(item.Subject))
            {
                AddError("Subject", "Subject can not be empty.");
            }
            if (!Util.IsValidInt(item.TaskStatus))
            {
                AddError("TaskStatus", "Task Status can not be empty.");
            }
            if (!Util.IsValidInt(item.TaskPriority))
            {
                AddError("TaskPriority", "Task Priority can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(TaskFilterModel filter)
        {
            TaskFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.AssignedToKey != filter.AssignedToKey)
                return true;

            if (defaultFilter.ObjectKey != filter.ObjectKey)
                return true;

            if (defaultFilter.TaskStatus != filter.TaskStatus)
                return true;

            if (defaultFilter.TaskPriority != filter.TaskPriority)
                return true;

            return false;
        }

        public virtual TaskFilterModel CreateFilter()
        {
            TaskFilterModel filter = new TaskFilterModel();

            return UpdateFilter(filter);
        }

        public virtual TaskFilterModel CreateFilter(TaskModel item)
        {
            TaskFilterModel filter = new TaskFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ResourceKey = item.ResourceKey;
            filter.AssignedToKey = item.AssignedToKey;
            filter.ObjectKey = item.ObjectKey;
            filter.TaskStatus = item.TaskStatus;
            filter.TaskPriority = item.TaskPriority;

            return UpdateFilter(filter);
        }

        public virtual TaskFilterModel UpdateFilter(TaskFilterModel filter)
        {
            return filter;
        }

        public virtual TaskModel Create()
        {
            ResetSiteSettings();

            TaskModel item = new TaskModel();

            return item;
        }

        public virtual bool Create(TaskModel item)
        {
            item.DateAdded = DateTime.Now;
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(TaskModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Update(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            return __repository.Delete(id);
        }

        public virtual TaskModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<TaskModel> GetAll()
        {
            TaskFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<TaskModel> GetAll(TaskFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<TaskModel> GetAll(TaskFilterModel filter, PagingModel paging)
        {
            IList<TaskModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }
    }
}
