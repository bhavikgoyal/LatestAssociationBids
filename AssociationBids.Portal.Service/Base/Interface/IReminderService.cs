using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IReminderService : IBaseService
    {
        bool Validate(ReminderModel item);
        bool IsFilterEnabled(ReminderFilterModel filter);
        ReminderFilterModel CreateFilter();
        ReminderFilterModel CreateFilter(ReminderModel item);
        ReminderFilterModel UpdateFilter(ReminderFilterModel filter);
        ReminderModel Create();
        bool Create(ReminderModel item);
        bool Update(ReminderModel item);
        bool Delete(int id);
        ReminderModel Get(int id);
        IList<ReminderModel> GetAll();
        IList<ReminderModel> GetAll(ReminderFilterModel filter);
        IList<ReminderModel> GetAll(ReminderFilterModel filter, PagingModel paging);
    }
}
