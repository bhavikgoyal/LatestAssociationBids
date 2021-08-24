using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ITaskService : IBaseService
    {
        bool Validate(TaskModel item);
        bool IsFilterEnabled(TaskFilterModel filter);
        TaskFilterModel CreateFilter();
        TaskFilterModel CreateFilter(TaskModel item);
        TaskFilterModel UpdateFilter(TaskFilterModel filter);
        TaskModel Create();
        bool Create(TaskModel item);
        bool Update(TaskModel item);
        bool Delete(int id);
        TaskModel Get(int id);
        IList<TaskModel> GetAll();
        IList<TaskModel> GetAll(TaskFilterModel filter);
        IList<TaskModel> GetAll(TaskFilterModel filter, PagingModel paging);
    }
}
