using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class MessageService : AssociationBids.Portal.Service.Base.MessageService, IMessageService
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
    }
}
