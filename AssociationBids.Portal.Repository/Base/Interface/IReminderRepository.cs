using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IReminderRepository : IBaseRepository
    {
        bool Create(ReminderModel item);
        bool Update(ReminderModel item);
        bool Delete(int id);
        ReminderModel Get(int id);
        IList<ReminderModel> GetAll();
        IList<ReminderModel> GetAll(ReminderFilterModel filter);
        IList<ReminderModel> GetAll(ReminderFilterModel filter, PagingModel paging);
    }
}
