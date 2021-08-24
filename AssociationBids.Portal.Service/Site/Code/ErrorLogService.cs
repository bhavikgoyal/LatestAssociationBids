using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class ErrorLogService : AssociationBids.Portal.Service.Base.ErrorLogService, IErrorLogService
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
    }
}
