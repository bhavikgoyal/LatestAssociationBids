using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class ReminderService : AssociationBids.Portal.Service.Site.ReminderService, IReminderService
    {
        new protected IReminderRepository __repository;

        public ReminderService()
            : this(new ReminderRepository()) { }

        public ReminderService(string connectionString)
            : this(new ReminderRepository(connectionString)) { }

        public ReminderService(IReminderRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override ReminderFilterModel UpdateFilter(ReminderFilterModel filter)
        {
            return filter;
        }
    }
}
