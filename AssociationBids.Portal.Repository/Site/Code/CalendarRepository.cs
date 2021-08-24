using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Site
{
    public class CalendarRepository : AssociationBids.Portal.Repository.Base.CalendarRepository, ICalendarRepository
    {
        public CalendarRepository() 
            : base() { }

        public CalendarRepository(string connectionString)
            : base(connectionString) { }
    }
}
