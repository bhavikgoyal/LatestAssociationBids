using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ICalendarRepository : IBaseRepository
    {
        bool Create(CalendarModel item);
        bool Update(CalendarModel item);
        bool Delete(int id);
        CalendarModel Get(int id);
        IList<CalendarModel> GetAll();
        IList<CalendarModel> GetAll(CalendarFilterModel filter);
        IList<CalendarModel> GetAll(CalendarFilterModel filter, PagingModel paging);
    }
}
