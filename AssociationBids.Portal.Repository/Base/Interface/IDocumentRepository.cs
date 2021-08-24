using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IDocumentRepository : IBaseRepository
    {
        bool Create(DocumentModel item);
        bool Update(DocumentModel item);
        bool Delete(int id);
        DocumentModel Get(int id);
        IList<DocumentModel> GetAll();
        IList<DocumentModel> GetAll(DocumentFilterModel filter);
        IList<DocumentModel> GetAll(DocumentFilterModel filter, PagingModel paging);
    }
}
