using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class DocumentService : AssociationBids.Portal.Service.Base.DocumentService, IDocumentService
    {
        new protected IDocumentRepository __repository;

        public DocumentService()
            : this(new DocumentRepository()) { }

        public DocumentService(string connectionString)
            : this(new DocumentRepository(connectionString)) { }

        public DocumentService(IDocumentRepository repository)
            : base(repository)
        {
            __repository = repository;
        }
    }
}
