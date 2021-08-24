using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class SessionService : BaseService, ISessionService
    {
        protected ISessionRepository __repository;

        public SessionService()
            : this(new SessionRepository()) { }

        public SessionService(string connectionString)
            : this(new SessionRepository(connectionString)) { }

        public SessionService(ISessionRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(SessionModel item)
        {
            if (!Util.IsValidGUID(item.SessionID))
            {
                AddError("SessionID", "Session can not be empty.");
            }
            if (!Util.IsValidText(item.Salt))
            {
                AddError("Salt", "Salt can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(SessionFilterModel filter)
        {
            SessionFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.SessionID != filter.SessionID)
                return true;

            return false;
        }

        public virtual SessionFilterModel CreateFilter()
        {
            SessionFilterModel filter = new SessionFilterModel();

            return UpdateFilter(filter);
        }

        public virtual SessionFilterModel CreateFilter(SessionModel item)
        {
            SessionFilterModel filter = new SessionFilterModel();
            filter.SessionID = item.SessionID;
            return UpdateFilter(filter);
        }

        public virtual SessionFilterModel UpdateFilter(SessionFilterModel filter)
        {
            return filter;
        }

        public virtual SessionModel Create()
        {
            ResetSiteSettings();

            SessionModel item = new SessionModel();

            return item;
        }

        public virtual bool Create(SessionModel item)
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

        public virtual bool Update(SessionModel item)
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

        public virtual SessionModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<SessionModel> GetAll()
        {
            SessionFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<SessionModel> GetAll(SessionFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<SessionModel> GetAll(SessionFilterModel filter, PagingModel paging)
        {
            IList<SessionModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
