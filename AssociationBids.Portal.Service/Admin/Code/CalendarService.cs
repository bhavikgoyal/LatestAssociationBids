using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class CalendarService : AssociationBids.Portal.Service.Site.CalendarService, ICalendarService
    {
        new protected ICalendarRepository __repository;

        public CalendarService()
            : this(new CalendarRepository()) { }

        public CalendarService(string connectionString)
            : this(new CalendarRepository(connectionString)) { }

        public CalendarService(ICalendarRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override CalendarFilterModel UpdateFilter(CalendarFilterModel filter)
        {
            return filter;
        }
    }
}
