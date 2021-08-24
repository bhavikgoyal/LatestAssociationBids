using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ICalendarService : IBaseService
    {
        bool Validate(CalendarModel item);
        bool IsFilterEnabled(CalendarFilterModel filter);
        CalendarFilterModel CreateFilter();
        CalendarFilterModel CreateFilter(CalendarModel item);
        CalendarFilterModel UpdateFilter(CalendarFilterModel filter);
        CalendarModel Create();
        bool Create(CalendarModel item);
        bool Update(CalendarModel item);
        bool Delete(int id);
        CalendarModel Get(int id);
        IList<CalendarModel> GetAll();
        IList<CalendarModel> GetAll(CalendarFilterModel filter);
        IList<CalendarModel> GetAll(CalendarFilterModel filter, PagingModel paging);
    }
}
