using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface INoteRepository : IBaseRepository
    {
        bool Create(NoteModel item);
        bool Update(NoteModel item);
        bool Delete(int id);
        NoteModel Get(int id);
        IList<NoteModel> GetAll();
        IList<NoteModel> GetAll(NoteFilterModel filter);
        IList<NoteModel> GetAll(NoteFilterModel filter, PagingModel paging);
    }
}
