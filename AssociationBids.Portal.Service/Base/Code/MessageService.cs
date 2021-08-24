using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class MessageService : BaseService, IMessageService
    {
        protected IMessageRepository __repository;

        public MessageService()
            : this(new MessageRepository()) { }

        public MessageService(string connectionString)
            : this(new MessageRepository(connectionString)) { }

        public MessageService(IMessageRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(MessageModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidInt(item.MessageStatus))
            {
                AddError("MessageStatus", "Message Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(MessageFilterModel filter)
        {
            MessageFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.ObjectKey != filter.ObjectKey)
                return true;

            if (defaultFilter.MessageStatus != filter.MessageStatus)
                return true;

            return false;
        }

        public virtual MessageFilterModel CreateFilter()
        {
            MessageFilterModel filter = new MessageFilterModel();

            return UpdateFilter(filter);
        }

        public virtual MessageFilterModel CreateFilter(MessageModel item)
        {
            MessageFilterModel filter = new MessageFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ResourceKey = item.ResourceKey;
            filter.ObjectKey = item.ObjectKey;
            filter.MessageStatus = item.MessageStatus;

            return UpdateFilter(filter);
        }

        public virtual MessageFilterModel UpdateFilter(MessageFilterModel filter)
        {
            return filter;
        }

        public virtual MessageModel Create()
        {
            ResetSiteSettings();

            MessageModel item = new MessageModel();

            return item;
        }

        public virtual bool Create(MessageModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(MessageModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Update(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            return __repository.Delete(id);
        }

        public virtual MessageModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<MessageModel> GetAll()
        {
            MessageFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<MessageModel> GetAll(MessageFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<MessageModel> GetAll(MessageFilterModel filter, PagingModel paging)
        {
            IList<MessageModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }
    }
}
