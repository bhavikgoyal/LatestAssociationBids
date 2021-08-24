using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class ErrorLogService : AssociationBids.Portal.Service.Site.ErrorLogService, IErrorLogService
    {
        new protected IErrorLogRepository __repository;

        public ErrorLogService()
            : this(new ErrorLogRepository()) { }

        public ErrorLogService(string connectionString)
            : this(new ErrorLogRepository(connectionString)) { }

        public ErrorLogService(IErrorLogRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override ErrorLogFilterModel UpdateFilter(ErrorLogFilterModel filter)
        {
            return filter;
        }
    }
}
