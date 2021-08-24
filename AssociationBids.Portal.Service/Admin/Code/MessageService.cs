using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class MessageService : AssociationBids.Portal.Service.Site.MessageService, IMessageService
    {
        new protected IMessageRepository __repository;

        public MessageService()
            : this(new MessageRepository()) { }

        public MessageService(string connectionString)
            : this(new MessageRepository(connectionString)) { }

        public MessageService(IMessageRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override MessageFilterModel UpdateFilter(MessageFilterModel filter)
        {
            return filter;
        }
    }
}
