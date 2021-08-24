using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface INoteService : IBaseService
    {
        bool Validate(NoteModel item);
        bool IsFilterEnabled(NoteFilterModel filter);
        NoteFilterModel CreateFilter();
        NoteFilterModel CreateFilter(NoteModel item);
        NoteFilterModel UpdateFilter(NoteFilterModel filter);
        NoteModel Create();
        bool Create(NoteModel item);
        bool Update(NoteModel item);
        bool Delete(int id);
        NoteModel Get(int id);
        IList<NoteModel> GetAll();
        IList<NoteModel> GetAll(NoteFilterModel filter);
        IList<NoteModel> GetAll(NoteFilterModel filter, PagingModel paging);
    }
}
