using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class EmailService : AssociationBids.Portal.Service.Site.EmailService, IEmailService
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

        public override EmailFilterModel UpdateFilter(EmailFilterModel filter)
        {
            return filter;
        }
    }
}
