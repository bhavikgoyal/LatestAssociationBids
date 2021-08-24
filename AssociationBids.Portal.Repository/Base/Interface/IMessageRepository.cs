using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IMessageRepository : IBaseRepository
    {
        bool Create(MessageModel item);
        bool Update(MessageModel item);
        bool Delete(int id);
        MessageModel Get(int id);
        IList<MessageModel> GetAll();
        IList<MessageModel> GetAll(MessageFilterModel filter);
        IList<MessageModel> GetAll(MessageFilterModel filter, PagingModel paging);
    }
}
