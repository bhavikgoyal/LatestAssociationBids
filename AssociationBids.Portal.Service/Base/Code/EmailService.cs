using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class EmailService : BaseService, IEmailService
    {
        protected IEmailRepository __repository;

        public EmailService()
            : this(new EmailRepository()) { }

        public EmailService(string connectionString)
            : this(new EmailRepository(connectionString)) { }

        public EmailService(IEmailRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(EmailModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidInt(item.EmailStatus))
            {
                AddError("EmailStatus", "Email Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(EmailFilterModel filter)
        {
            EmailFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.ObjectKey != filter.ObjectKey)
                return true;

            if (defaultFilter.EmailStatus != filter.EmailStatus)
                return true;

            return false;
        }

        public virtual EmailFilterModel CreateFilter()
        {
            EmailFilterModel filter = new EmailFilterModel();

            return UpdateFilter(filter);
        }

        public virtual EmailFilterModel CreateFilter(EmailModel item)
        {
            EmailFilterModel filter = new EmailFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ResourceKey = item.ResourceKey;
            filter.ObjectKey = item.ObjectKey;
            filter.EmailStatus = item.EmailStatus;

            return UpdateFilter(filter);
        }

        public virtual EmailFilterModel UpdateFilter(EmailFilterModel filter)
        {
            return filter;
        }

        public virtual EmailModel Create()
        {
            ResetSiteSettings();

            EmailModel item = new EmailModel();

            return item;
        }

        public virtual bool Create(EmailModel item)
        {
            item.DateAdded = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(EmailModel item)
        {
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

        public virtual EmailModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<EmailModel> GetAll()
        {
            EmailFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<EmailModel> GetAll(EmailFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<EmailModel> GetAll(EmailFilterModel filter, PagingModel paging)
        {
            IList<EmailModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
