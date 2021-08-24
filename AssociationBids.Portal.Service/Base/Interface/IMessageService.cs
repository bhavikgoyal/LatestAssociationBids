using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IMessageService : IBaseService
    {
        bool Validate(MessageModel item);
        bool IsFilterEnabled(MessageFilterModel filter);
        MessageFilterModel CreateFilter();
        MessageFilterModel CreateFilter(MessageModel item);
        MessageFilterModel UpdateFilter(MessageFilterModel filter);
        MessageModel Create();
        bool Create(MessageModel item);
        bool Update(MessageModel item);
        bool Delete(int id);
        MessageModel Get(int id);
        IList<MessageModel> GetAll();
        IList<MessageModel> GetAll(MessageFilterModel filter);
        IList<MessageModel> GetAll(MessageFilterModel filter, PagingModel paging);
    }
}
