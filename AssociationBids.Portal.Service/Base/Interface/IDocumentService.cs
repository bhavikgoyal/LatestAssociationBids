using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IDocumentService : IBaseService
    {
        bool Validate(DocumentModel item);
        bool IsFilterEnabled(DocumentFilterModel filter);
        DocumentFilterModel CreateFilter();
        DocumentFilterModel CreateFilter(DocumentModel item);
        DocumentFilterModel UpdateFilter(DocumentFilterModel filter);
        DocumentModel Create();
        bool Create(DocumentModel item);
        bool Update(DocumentModel item);
        bool Delete(int id);
        DocumentModel Get(int id);
        IList<DocumentModel> GetAll();
        IList<DocumentModel> GetAll(DocumentFilterModel filter);
        IList<DocumentModel> GetAll(DocumentFilterModel filter, PagingModel paging);
    }
}
