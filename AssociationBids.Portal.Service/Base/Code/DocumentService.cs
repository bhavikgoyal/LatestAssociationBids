using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class DocumentService : BaseService, IDocumentService
    {
        protected IDocumentRepository __repository;

        public DocumentService()
            : this(new DocumentRepository()) { }

        public DocumentService(string connectionString)
            : this(new DocumentRepository(connectionString)) { }

        public DocumentService(IDocumentRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(DocumentModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidText(item.FileName))
            {
                AddError("FileName", "File Name can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(DocumentFilterModel filter)
        {
            DocumentFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            if (defaultFilter.ObjectKey != filter.ObjectKey)
                return true;

            return false;
        }

        public virtual DocumentFilterModel CreateFilter()
        {
            DocumentFilterModel filter = new DocumentFilterModel();

            return UpdateFilter(filter);
        }

        public virtual DocumentFilterModel CreateFilter(DocumentModel item)
        {
            DocumentFilterModel filter = new DocumentFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ObjectKey = item.ObjectKey;

            return UpdateFilter(filter);
        }

        public virtual DocumentFilterModel UpdateFilter(DocumentFilterModel filter)
        {
            return filter;
        }

        public virtual DocumentModel Create()
        {
            ResetSiteSettings();

            DocumentModel item = new DocumentModel();

            return item;
        }

        public virtual bool Create(DocumentModel item)
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

        public virtual bool Update(DocumentModel item)
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

        public virtual DocumentModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<DocumentModel> GetAll()
        {
            DocumentFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<DocumentModel> GetAll(DocumentFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<DocumentModel> GetAll(DocumentFilterModel filter, PagingModel paging)
        {
            IList<DocumentModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
