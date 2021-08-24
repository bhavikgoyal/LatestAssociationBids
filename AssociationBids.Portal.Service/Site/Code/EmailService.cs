using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class EmailService : AssociationBids.Portal.Service.Base.EmailService, IEmailService
    {
        new protected IEmailRepository __repository;

        public EmailService()
            : this(new EmailRepository()) { }

        public EmailService(string connectionString)
            : this(new EmailRepository(connectionString)) { }

        public EmailService(IEmailRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
