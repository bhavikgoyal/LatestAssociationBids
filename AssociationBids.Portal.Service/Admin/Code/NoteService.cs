using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Admin;

namespace AssociationBids.Portal.Service.Admin
{
    public class NoteService : AssociationBids.Portal.Service.Site.NoteService, INoteService
    {
        new protected INoteRepository __repository;

        public NoteService()
            : this(new NoteRepository()) { }

        public NoteService(string connectionString)
            : this(new NoteRepository(connectionString)) { }

        public NoteService(INoteRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

        public override NoteFilterModel UpdateFilter(NoteFilterModel filter)
        {
            return filter;
        }
    }
}
