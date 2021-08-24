using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ITaskRepository : IBaseRepository
    {
        bool Create(TaskModel item);
        bool Update(TaskModel item);
        bool Delete(int id);
        TaskModel Get(int id);
        IList<TaskModel> GetAll();
        IList<TaskModel> GetAll(TaskFilterModel filter);
        IList<TaskModel> GetAll(TaskFilterModel filter, PagingModel paging);
    }
}
